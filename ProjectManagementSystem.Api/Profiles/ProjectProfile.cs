using System.Globalization;
using AutoMapper;
using ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;
using ProjectManagementSystem.Api.CQRS.Project.Commands.UpdateProject;
using ProjectManagementSystem.Api.CQRS.Project.Queries.GetList;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectDto, CreateProjectCommand>();
        CreateMap<Project, ProjectDto>()
            .ForMember(dest=>dest.CreationTime,opt=>opt.MapFrom(src=>src.CreationTime.ToString(CultureInfo.CurrentCulture)));

        CreateMap<UpdateProjectDto, UpdateProjectCommand>();
        CreateMap<UpdateProjectCommand, Project>();

        //CreateMap<GetProjectListQueryParams, GetProjectsQuery>();
    }
}
