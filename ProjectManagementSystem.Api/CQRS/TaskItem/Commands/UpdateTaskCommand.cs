using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.Linq;
using ProjectManagementSystem.Api.Exceptions.Error;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands
{
    public class UpdateTaskCommand : IRequest<ResultViewModel<TaskItemDto>>
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, 2)]
        public TaskItemStatus TaskStatus { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ResultViewModel<TaskItemDto>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRepository<Models.TaskItem> _taskRepository;
        private readonly IRepository<Models.Project> _projectRepository;
        private readonly IRepository<Models.User> _userRepository;

        public UpdateTaskCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IRepository<Models.TaskItem> taskRepository,
            IRepository<Models.Project> projectRepository,
            IRepository<Models.User> userRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel<TaskItemDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the task
            var task = _taskRepository.GetByID(request.TaskId);
            if (task == null)
            {
                return ResultViewModel<TaskItemDto>.Failure(ErrorCode.ResourceNotFound, "Task not found");
            }

            // Update the task's properties
            task.Title = request.Title;
            task.Description = request.Description;
            task.TaskStatus = request.TaskStatus;

            // Check if the UserId needs to be updated
            if (task.UserId != request.UserId)
            {
                var newUser = _userRepository.GetByID(request.UserId);
                if (newUser == null)
                {
                    return ResultViewModel<TaskItemDto>.Failure(ErrorCode.ResourceNotFound, "User not found");
                }

                task.UserId = request.UserId;
            }

            // Save the changes to the task
            _taskRepository.Update(task);
            _taskRepository.SaveChanges();

            // Fetch related project and user details
            var project = _projectRepository.GetByID(task.ProjectId);
            var user = _userRepository.GetByID(task.UserId);

            // Build the response DTO
            var taskItemDto = _mapper.Map<TaskItemDto>(task);
            taskItemDto.ProjectName = project?.Name;
            taskItemDto.UserName = user?.UserName;

            return ResultViewModel<TaskItemDto>.Success(taskItemDto, "Task updated successfully");
        }
    }
}

