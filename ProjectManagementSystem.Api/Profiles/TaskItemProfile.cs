using AutoMapper;
using ProjectManagementSystem.Api.CQRS.TaskItem.Commands;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using ProjectManagementSystem.Api.ViewModels.TaskItemVM;

namespace ProjectManagementSystem.Api.Profiles
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile()
        {
            CreateMap<TaskItemDto, TaskItemVM>();
            CreateMap<CreateTaskDto, CreateTaskCommand>()
             .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ProjectIds, opt => opt.MapFrom(src => src.ProjectIds))
            .ForMember(dest => dest.AssignedUserIds, opt => opt.MapFrom(src => src.AssignedUserIds));

            CreateMap<TaskItem, TaskItemDto>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => src.TaskStatus));

            CreateMap<ResultViewModel<TaskItemDto>, TaskItemVM>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Data.Id))
          .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Data.Title))
          .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Data.ProjectName))
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Data.UserName))
          .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => src.Data.TaskStatus));

        }
    }
}
