using AutoMapper;
using ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectDto, CreateProjectCommand>();
        CreateMap<Project, ProjectDto>();

    }
}
