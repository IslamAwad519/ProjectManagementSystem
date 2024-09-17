using MediatR;
using ProjectManagementSystem.Api.DTOs.Projects;

namespace ProjectManagementSystem.Api.CQRS.Project.Queries.GetList;

public class GetProjectsQuery : IRequest<List<ProjectDto>>
{
}