using MediatR;
using ProjectManagementSystem.Api.DTOs.ResultDTO;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands
{
    public record ChangeTaskStatusCommand(TaskItemStatusDTO taskStatusDTO) : IRequest<ResultDTO>;

    public record TaskItemStatusDTO(int taskID, TaskItemStatus tasksStatus);

    public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, ResultDTO>
    {
        private readonly IRepository<Models.TaskItem> _repository;
        public ChangeTaskStatusCommandHandler( IRepository<Models.TaskItem> repository)
        {
            _repository = repository;
             
        }
        public async Task<ResultDTO> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task =await _repository.FirstAsync(t => request.taskStatusDTO.taskID == t.Id && t.TaskStatus == request.taskStatusDTO.tasksStatus);
            if (task != null) {
                return ResultDTO.Failure($"Task status is already {request.taskStatusDTO.tasksStatus}!");
            }

            task.TaskStatus = request.taskStatusDTO.tasksStatus;

            await _repository.UpdateAsync(task);
            await _repository.SaveChangesAsync();

            return  ResultDTO.Success(task, "Task Updated successfully!");



        }
    }




}
