using MediatR;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest<Result>
    {
        public int TaskId { get; set; }
    }

  
}
