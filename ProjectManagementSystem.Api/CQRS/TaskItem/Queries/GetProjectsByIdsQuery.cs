using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries
{
    public class GetProjectsByIdsQuery : IRequest<List<ProjectDto>>
    {
        public List<int> ProjectIds { get; set; } // Make sure this matches your query needs
    }

    public class GetProjectsByIdsQueryHandler : IRequestHandler<GetProjectsByIdsQuery, List<ProjectDto>>
    {
        private readonly IRepository<Models.Project> _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectsByIdsQueryHandler(IRepository<Models.Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsByIdsQuery request, CancellationToken cancellationToken)
        {
            // Use the Get method with a predicate to filter projects by IDs
            var projects = _projectRepository.Get(p => request.ProjectIds.Contains(p.Id)).ToList();
            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
            return projectDtos;
        }
    }

}


