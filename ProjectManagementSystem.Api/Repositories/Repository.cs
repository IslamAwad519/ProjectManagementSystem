using ProjectManagementSystem.Api.Data;
using ProjectManagementSystem.Api.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.Repositories;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        //_context.Set<T>().Remove(entity);
        entity.IsDeleted = true;
        Update(entity);
    }

    public void Delete(int id)
    {
        T entity = _context.Find<T>(id);
        Delete(entity);
    }

    public void HardDelete(int id)
    {
        //T entity = _context.Find<T>(id);
        //_context.Set<T>().Remove(entity);

        _context.Set<T>().Where(x => x.Id == id).ExecuteDelete();
    }

    public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
    {
        return await GetAll().Where(predicate).FirstOrDefaultAsync();
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
    {
        return GetAll().Where(predicate);
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().Where(x => !x.IsDeleted).AsNoTracking();
        //return _context.Set<T>().Where(x => !x.Deleted).AsNoTrackingWithIdentityResolution();
    }

    public T GetByID(int id)
    {
        //return _context.Find<T>(id);
        return GetAll().FirstOrDefault(x => x.Id == id);
    }

    public T GetWithTrackinByID(int id)
    {
        return _context.Set<T>()
            .Where(x => !x.IsDeleted && x.Id == id)
            .AsTracking()
            .FirstOrDefault();
    }

    public T First(Expression<Func<T, bool>> predicate)
    {
        return Get(predicate).FirstOrDefault();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return await Task.FromResult(entity);
    }
}
