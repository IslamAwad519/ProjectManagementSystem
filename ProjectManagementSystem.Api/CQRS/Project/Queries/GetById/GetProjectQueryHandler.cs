using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.Project.Queries.GetById;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectDto>
{
    private readonly IRepository<Models.Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectQueryHandler(IRepository<Models.Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project =  _projectRepository.GetByID(request.ProjectId);
        return _mapper.Map<ProjectDto>(project);
    }
}

