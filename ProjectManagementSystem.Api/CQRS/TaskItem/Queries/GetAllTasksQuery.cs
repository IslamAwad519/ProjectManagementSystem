using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using TaskStatusEnum = ProjectManagementSystem.Api.Enums.TaskItemStatus;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Api.Enums;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries
{
    public class GetAllTasksQuery : IRequest<List<TaskItemDto>>
    {
    
    }

    public class GetAllTasksInAllProjectsQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskItemDto>>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;

        public GetAllTasksInAllProjectsQueryHandler(IRepository<Models.TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskItemDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var query = _taskRepository.GetAll();

            var tasks = await query.Select(task => new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                TaskStatus = task.TaskStatus, 
                ProjectName = task.Project.Name,
                UserName = task.User.UserName,
                DateCreated = task.CreatedAt,
            }).ToListAsync(cancellationToken);

            // Convert the TaskStatus enum to a string representation
            var tasksDto = tasks.Select(task => new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                TaskStatus = task.TaskStatus,
                ProjectName = task.ProjectName,
                UserName = task.UserName,
                DateCreated = task.DateCreated
            }).ToList();

            return tasksDto;
        }
    }
}




