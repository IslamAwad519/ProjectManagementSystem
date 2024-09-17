using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.Project.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto>
{
    private readonly IRepository<Models.Project> _projectRepository;
    private readonly IRepository<Models.User> _userRepository;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(
        IRepository<Models.Project> projectRepository,
        IRepository<Models.User> userRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project =  _projectRepository.GetByID(request.Id);
        if (project == null)
        {
            return null;
        }

        //var users = await _userRepository.GetUsersByIdsAsync(request.UserIds);
       project =  _mapper.Map(request,project);
        //project.Users = users.Select(u => new ProjectUser { UserId = u.Id }).ToList();

         _projectRepository.Update(project);
         _projectRepository.SaveChanges();

        return _mapper.Map<ProjectDto>(project);
    }
}
