using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries
{
    public class GetUserListForTasksQuery : IRequest<List<DTOs.Tasks.UserDto>>
    {
        public List<int> ProjectIds { get; set; }
    }


    public class GetUserListForTasksHandler : IRequestHandler<GetUserListForTasksQuery, List<DTOs.Tasks.UserDto>>
    {
        private readonly IRepository<ProjectUser> _projectUserRepository;
        private readonly IMapper _mapper;

        public GetUserListForTasksHandler(IRepository<ProjectUser> projectUserRepository, IMapper mapper)
        {
            _projectUserRepository = projectUserRepository;
            _mapper = mapper;
        }

        public Task<List<DTOs.Tasks.UserDto>> Handle(GetUserListForTasksQuery request, CancellationToken cancellationToken)
        {
            // Use the asynchronous Get method and filter by checking if ProjectId is in the list
            var projectUsers = _projectUserRepository.Get(pu => request.ProjectIds.Contains(pu.ProjectId))
                                                     .Select(pu => pu.User) // ProjectUser.User navigation property
                                                     .ToList(); 

            var userDtos = projectUsers.Select(user => _mapper.Map<DTOs.Tasks.UserDto>(user)).ToList();

            return Task.FromResult(userDtos);
        }
    }
}
