using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries
{
    public class GetUsersByIdsQuery : IRequest<List<UserDto>>
    {
        public List<int> UserIds { get; set; }
    }

    public class GetUsersByIdsQueryHandler : IRequestHandler<GetUsersByIdsQuery, List<UserDto>>
    {
        private readonly IRepository<Models.User> _userRepository;
        private readonly IMapper _mapper;

        public GetUsersByIdsQueryHandler(IRepository<Models.User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
        {
            var users = _userRepository.Get(u => request.UserIds.Contains(u.Id)).ToList();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }
    }

}


