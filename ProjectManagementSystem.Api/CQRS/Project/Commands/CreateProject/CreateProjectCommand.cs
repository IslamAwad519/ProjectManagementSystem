using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Result<ProjectDto>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    //public ICollection<int> UserIds { get; set; }  // Assuming you pass user IDs to associate with the project
}