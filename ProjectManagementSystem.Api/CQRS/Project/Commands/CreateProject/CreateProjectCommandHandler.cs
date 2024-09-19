using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
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

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {

       var project =  _mapper.Map<Models.Project>(request);

        _projectRepository.Add(project);
         _projectRepository.SaveChanges();

        return _mapper.Map<ProjectDto>(project);
    }
}
