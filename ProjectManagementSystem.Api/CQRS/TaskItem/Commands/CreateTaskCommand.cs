using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagementSystem.Api.CQRS.TaskItem.Queries;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Commands
{
    public class CreateTaskCommand : IRequest<ResultViewModel<TaskItemDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> ProjectIds { get; set; }
        public List<int> AssignedUserIds { get; set; }

        public CreateTaskCommand(string title, string description, List<int> projectIds, List<int> assignedUserIds)
        {
            Title = title;
            Description = description;
            ProjectIds = projectIds;
            AssignedUserIds = assignedUserIds;
        }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, ResultViewModel<TaskItemDto>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRepository<Models.TaskItem> _taskRepository;

        public CreateTaskCommandHandler(IMediator mediator, IMapper mapper, IRepository<Models.TaskItem> taskRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<ResultViewModel<TaskItemDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            // Fetch projects
            var projects = await _mediator.Send(new GetProjectsByIdsQuery { ProjectIds = request.ProjectIds });
            if (!projects.Any())
            {
                return ResultViewModel<TaskItemDto>.Failure(ErrorCode.ResourceNotFound, "No valid projects found");
            }

            // Fetch users
            var users = await _mediator.Send(new GetUsersByIdsQuery { UserIds = request.AssignedUserIds });
            if (users.Count != request.AssignedUserIds.Count)
            {
                return ResultViewModel<TaskItemDto>.Failure(ErrorCode.ResourceNotFound, "One or more users are not valid members of the selected projects");
            }

            var task = new Models.TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                ProjectId = projects.First().Id,
                UserId = users.FirstOrDefault()?.Id ?? throw new Exception("No valid user found"),
                TaskStatus = TaskItemStatus.ToDo,
                CreatedAt = DateTime.Now
            };

            _taskRepository.Add(task);
           _taskRepository.SaveChanges(); 
            var taskItemDto = _mapper.Map<TaskItemDto>(task);

            taskItemDto.ProjectName = projects.First().Name;
            taskItemDto.UserName = string.Join(", ", users.Select(u => u.UserName));

            return ResultViewModel<TaskItemDto>.Success(taskItemDto, "Task created successfully");
        }
    }
}


