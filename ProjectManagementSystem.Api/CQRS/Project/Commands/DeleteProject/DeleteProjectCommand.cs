using MediatR;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.Project.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest<Result>
{
    public int ProjectId { get; set; }
}

