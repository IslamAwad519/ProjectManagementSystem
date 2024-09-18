using MediatR;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands
{
    public class DeleteTaskCommand : IRequest<ResultViewModel<bool>>
    {
        public int TaskId { get; set; }

        public DeleteTaskCommand(int taskId)
        {
            TaskId = taskId;
        }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ResultViewModel<bool>>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;

        public DeleteTaskCommandHandler(IRepository<Models.TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultViewModel<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _taskRepository.GetByID(request.TaskId);

            if (task == null)
            {
                return ResultViewModel<bool>.Failure(ErrorCode.ResourceNotFound, "Task not found.");
            }

            _taskRepository.Delete(task);
            _taskRepository.SaveChanges();

            return ResultViewModel<bool>.Success(true, "Task deleted successfully.");
        }
    }
}
