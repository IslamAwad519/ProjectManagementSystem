using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries
{
    public class SearchTaskByTaskNameQuery : IRequest<ResultViewModel<List<TaskItemDto>>>
    {
        public string TaskName { get; }

        public SearchTaskByTaskNameQuery(string taskName)
        {
            TaskName = taskName;
        }
    }


    public class SearchTaskByTaskNameQueryHandler : IRequestHandler<SearchTaskByTaskNameQuery, ResultViewModel<List<TaskItemDto>>>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;
        private readonly IRepository<Models.Project> _projectRepository;
        private readonly IRepository<Models.User> _userRepository;

        public SearchTaskByTaskNameQueryHandler(IRepository<Models.TaskItem> taskRepository, IRepository<Models.Project> projectRepository, IRepository<Models.User> userRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel<List<TaskItemDto>>> Handle(SearchTaskByTaskNameQuery request, CancellationToken cancellationToken)
        {
            var tasks = _taskRepository.GetAll()
                .Where(t => t.Title.Contains(request.TaskName))
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    TaskStatus = t.TaskStatus,
                    ProjectName = t.Project.Name,
                    UserName = t.User.UserName,
                    DateCreated = t.CreationTime
                }).ToList();

            if (tasks.Any())
            {
                return ResultViewModel<List<TaskItemDto>>.Success(tasks, "Tasks found successfully.");
            }

            return ResultViewModel<List<TaskItemDto>>.Failure(ErrorCode.ResourceNotFound, "No tasks found in This Name.");
        }
    }

}
