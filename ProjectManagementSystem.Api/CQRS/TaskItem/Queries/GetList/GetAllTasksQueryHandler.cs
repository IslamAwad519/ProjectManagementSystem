using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries.GetList
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, Result<List<TaskItemDto>>>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;
        private readonly IMapper _mapper;

        public GetAllTasksQueryHandler(IRepository<Models.TaskItem> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<TaskItemDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var query = _taskRepository.GetAll();

            // Filtering by Task Name
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(t => t.Title.ToLower().Contains(request.Name.ToLower()));
            }

            // Filtering by Task Status
            if (request.Status.HasValue)
            {
                query = query.Where(t => t.TaskStatus == request.Status.Value);
            }

            // Filtering by CreatedFrom and CreatedTo dates
            if (request.CreatedFrom.HasValue || request.CreatedTo.HasValue)
            {
                if (request.CreatedFrom.HasValue && request.CreatedTo.HasValue)
                {
                    query = query.Where(t => t.CreationTime >= request.CreatedFrom.Value && t.CreationTime <= request.CreatedTo.Value);
                }
                else if (request.CreatedFrom.HasValue)
                {
                    query = query.Where(t => t.CreationTime >= request.CreatedFrom.Value);
                }
                else if (request.CreatedTo.HasValue)
                {
                    query = query.Where(t => t.CreationTime <= request.CreatedTo.Value);
                }
            }

            // Ordering the results
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                query = request.OrderBy.ToLower() switch
                {
                    "name" => request.IsDescending ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
                    "creationtime" => request.IsDescending ? query.OrderByDescending(t => t.CreationTime) : query.OrderBy(t => t.CreationTime),
                    _ => query
                };
            }

            // Include User and Project information
            query = query.Include(t => t.User)
                         .Include(t => t.Project);

            var tasks = await query.ToListAsync();
            var tasksDto = _mapper.Map<List<TaskItemDto>>(tasks);

            if (tasksDto.Count == 0)
            {
                return Result<List<TaskItemDto>>.Failure("No tasks found matching your criteria");
            }

            return Result<List<TaskItemDto>>.Success(tasksDto, "Tasks found successfully.");
        }
    }
}
