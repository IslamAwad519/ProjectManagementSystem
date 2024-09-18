using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskItemDto>
    {
        public int Id { get; set; }
    }

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskItemDto>
    {
        private readonly IRepository<Models.TaskItem> _taskRepository;
        private readonly IMapper _mapper;

        public GetTaskByIdQueryHandler(IRepository<Models.TaskItem> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskItemDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            // Execute the query and await the result
            var task = await _taskRepository.GetAll()
                .Where(t => t.Id == request.Id)
                .Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.TaskStatus,
                    ProjectName = t.Project.Name,  
                    UserName = t.User.UserName,    
                    t.CreationTime
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (task == null)
            {
                return null;
            }

            // Map the result to DTO
            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                TaskStatus = task.TaskStatus,
                ProjectName = task.ProjectName,
                UserName = task.UserName,
                DateCreated = task.CreationTime
            };
        }
    }
}
