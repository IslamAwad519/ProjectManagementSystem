using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.Project.Queries.GetById;

public class GetProjectQuery : IRequest<ProjectDto>
{
    public int ProjectId { get; set; }
}

