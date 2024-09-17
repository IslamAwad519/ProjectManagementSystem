using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Repositories.Interfaces;

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
        var projects =  _projectRepository.GetAll();

        return _mapper.Map<List<ProjectDto>>(projects);
    }
}

