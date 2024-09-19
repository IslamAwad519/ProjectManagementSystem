using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<ProjectDto>>
{
    private readonly IRepository<Models.Project> _projectRepository;  // Use an abstraction for your data access
    private readonly IRepository<Models.User> _userRepository;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(
        IRepository<Models.Project> projectRepository,
        IRepository<Models.User> userRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProjectDto>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {

       var project =  _mapper.Map<Models.Project>(request);

        _projectRepository.Add(project);
         _projectRepository.SaveChanges();

        var projectDto =  _mapper.Map<ProjectDto>(project);
        if (projectDto == null)
        {
            return Result<ProjectDto>.Failure($"creating the project failed");
        }

        return Result<ProjectDto>.Success(projectDto, "Project created successfully.");
    }
}
