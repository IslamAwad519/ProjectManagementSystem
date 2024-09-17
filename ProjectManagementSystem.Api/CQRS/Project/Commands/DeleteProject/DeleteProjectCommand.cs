using MediatR;

namespace ProjectManagementSystem.Api.CQRS.Project.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest<bool>
{
    public int ProjectId { get; set; }
}

