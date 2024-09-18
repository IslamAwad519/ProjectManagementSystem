using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries
{
    public class FilterTasksQuery : IRequest<ResultViewModel<List<TaskItemDto>>>
    {
        public string? ProjectName { get; set; }
        [Range(0, 2)]
        public TaskItemStatus? TaskStatus { get; set; }

        public FilterTasksQuery(string? projectName, TaskItemStatus? taskStatus)
        {
            ProjectName = projectName;
            TaskStatus = taskStatus;
        }
    }


    public class FilterTasksQueryHandler : IRequestHandler<FilterTasksQuery, ResultViewModel<List<TaskItemDto>>>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;

        public FilterTasksQueryHandler(IRepository<Models.TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ResultViewModel<List<TaskItemDto>>> Handle(FilterTasksQuery request, CancellationToken cancellationToken)
        {
           
            var tasks = _taskRepository.GetAll();

            // Filter by ProjectName if provided
            if (!string.IsNullOrEmpty(request.ProjectName))
            {
                tasks = tasks.Where(t => t.Project != null && t.Project.Name.Contains(request.ProjectName));
            }

            // Filter by TaskStatus if provided And Cast the TaskStatus before i make the compairson
            if (request.TaskStatus.HasValue)
            {
                tasks = tasks.Where(t => t.TaskStatus == (TaskItemStatus)request.TaskStatus.Value);
            }

            var taskDtos = tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                TaskStatus = t.TaskStatus,
                ProjectName = t.Project.Name,
                UserName = t.User.UserName,
                DateCreated = t.CreationTime
            }).ToList();

            if (taskDtos.Any())
            {
                return ResultViewModel<List<TaskItemDto>>.Success(taskDtos, "Tasks found successfully.");
            }
            return ResultViewModel<List<TaskItemDto>>.Failure(ErrorCode.ResourceNotFound, "No tasks found with the given filters.");
        }
    }
}
