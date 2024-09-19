using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.Project.Queries.GetList;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    private readonly IRepository<Models.Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IRepository<Models.Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var query =  _projectRepository.GetAll();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(request.Name.ToLower()));
        }

        if (request.Status.HasValue)
        {
            query = query.Where(p => p.ProjectStatus == request.Status.Value);
        }

        if (request.CreatedFrom.HasValue || request.CreatedTo.HasValue)
        {
            if (request.CreatedFrom.HasValue && request.CreatedTo.HasValue)
            {
                query = query.Where(c => c.CreationTime >= request.CreatedFrom.Value && c.CreationTime <= request.CreatedTo.Value);
            }
            else if (request.CreatedFrom.HasValue)
            {
                query = query.Where(w => w.CreationTime >= request.CreatedFrom.Value);
            }
            else if(request.CreatedTo.HasValue)
            {
                query = query.Where(x => x.CreationTime <= request.CreatedTo.Value);
            }
        }
        
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query = request.OrderBy.ToLower() switch
            {
                "name" => request.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "creationtime" => request.IsDescending ? query.OrderByDescending(p => p.CreationTime) : query.OrderBy(p => p.CreationTime),
                _ => query 
            };
        }
        var projects = query.ToList();
        return _mapper.Map<List<ProjectDto>>(projects);
    }
}

