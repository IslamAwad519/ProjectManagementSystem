using AutoMapper;
using ProjectManagementSystem.Api.CQRS.Project.Commands.CreateProject;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands.CreateTask;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands.UpdateTask;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Enums;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;


namespace ProjectManagementSystem.Api.Profiles
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile()
        {
           
            CreateMap<CreateTaskCommand, TaskItem>()
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => Enums.TaskItemStatus.ToDo))
              .AfterMap((src, dest) =>
              {
                  dest.ProjectId = src.ProjectIds.FirstOrDefault(); 
                  dest.UserId = src.AssignedUserIds.FirstOrDefault(); 
              });

            CreateMap<TaskItem, TaskItemDto>()
            .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => src.TaskStatus)) 
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name)) 
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) 
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.CreationTime));


            CreateMap<CreateTaskDto, CreateTaskCommand>()
           .ForMember(dest => dest.ProjectIds, opt => opt.MapFrom(src => src.ProjectIds)) 
           .ForMember(dest => dest.AssignedUserIds, opt => opt.MapFrom(src => src.AssignedUserIds))
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));
    

            CreateMap<UpdateTaskDto, UpdateTaskCommand>()
                       .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                       .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                       .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                       .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                       .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => src.TaskStatus));
            CreateMap<UpdateTaskCommand, TaskItem>();

        }
    }
}
