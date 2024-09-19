using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.Project.Queries.GetById;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, Result<ProjectDto>>
{
    private readonly IRepository<Models.Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectQueryHandler(IRepository<Models.Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProjectDto>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project =  _projectRepository.GetByID(request.ProjectId);
        if (project == null)
        {
            return Result<ProjectDto>.Failure($"No project with id {request.ProjectId} found");
        }

        var projectDto =  _mapper.Map<ProjectDto>(project);

        return Result<ProjectDto>.Success(projectDto, "Project found successfully.");
    }
}

