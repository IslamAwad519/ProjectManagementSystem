using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries.GetById
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskItemDto>>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;
        private readonly IRepository<Models.Project> _projectRepository;
        private readonly IRepository<Models.User> _userRepository;
        private readonly IMapper _mapper;

        public GetTaskByIdQueryHandler(IRepository<Models.TaskItem> taskRepository, IRepository<Models.Project> projectRepository, IRepository<Models.User> userRepository, IMapper mapper) 
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Result<TaskItemDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch the task including related Project and User
            var task = await _taskRepository
                .GetAll()
                .Include(t => t.Project)  
                .Include(t => t.User)    
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
            {
                return Result<TaskItemDto>.Failure("Task not found.");
            }

            var taskDto = _mapper.Map<TaskItemDto>(task);

            return Result<TaskItemDto>.Success(taskDto, "Task retrieved successfully.");
        }


    }
}
