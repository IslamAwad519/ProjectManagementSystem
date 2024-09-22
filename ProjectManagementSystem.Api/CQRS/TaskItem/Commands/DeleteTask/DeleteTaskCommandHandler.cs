using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;
        private readonly IMapper _mapper;

        public DeleteTaskCommandHandler(IRepository<Models.TaskItem> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _taskRepository.GetByID(request.TaskId);
            if (task == null)
            {
                return Result.Failure($"No task with ID: {request.TaskId}");
            }

            _taskRepository.Delete(task);
            _taskRepository.SaveChanges();

            return Result.Success("Task deleted successfully");
        }
    }
}
