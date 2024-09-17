using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;

namespace ProjectManagementSystem.Api.CQRS.Project.Queries.GetById;

public class GetProjectQuery : IRequest<ProjectDto>
{
    public int ProjectId { get; set; }
}

