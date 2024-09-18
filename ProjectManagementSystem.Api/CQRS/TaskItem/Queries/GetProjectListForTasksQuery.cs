using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.TaskItem.Queries
{
    public class GetProjectListForTasksQuery : IRequest<List<ProjectDto>> { }

    public class GetProjectListForTasksHandler : IRequestHandler<GetProjectListForTasksQuery, List<ProjectDto>>
    {
        private readonly IRepository<Models.Project> _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectListForTasksHandler(IRepository<Models.Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public Task<List<ProjectDto>> Handle(GetProjectListForTasksQuery request, CancellationToken cancellationToken)
        {
            var projects = _projectRepository.GetAll().ToList();
            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
            return Task.FromResult(projectDtos);
        }
    }
}

