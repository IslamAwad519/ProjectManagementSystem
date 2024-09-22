using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result<TaskItemDto>>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;
        private readonly IRepository<Models.User> _userRepository;
        private readonly IRepository<Models.Project> _projectRepository;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(
            IRepository<Models.TaskItem> taskRepository,
            IRepository<Models.User> userRepository,
            IRepository<Models.Project> projectRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<Result<TaskItemDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            // Fetch TaskItem by ID
            var taskItem = _taskRepository.GetByID(request.TaskId);

            if (taskItem == null)
            {
                return Result<TaskItemDto>.Failure("Task not found.");
            }

            // Map updated fields from command to TaskItem
            _mapper.Map(request, taskItem);

            // Update the task item
            _taskRepository.Update(taskItem);
            _taskRepository.SaveChanges();

            // Fetch related Project and User based on the provided IDs
            var project = _projectRepository.GetByID(request.ProjectId); 
            var user = _userRepository.GetByID(request.UserId); 

            //assign the names from the related entities
            var taskItemDto = _mapper.Map<TaskItemDto>(taskItem);
            taskItemDto.ProjectName = project?.Name; 
            taskItemDto.UserName = user?.UserName; 

            return Result<TaskItemDto>.Success(taskItemDto, "Task updated successfully.");
        }
    }



}


