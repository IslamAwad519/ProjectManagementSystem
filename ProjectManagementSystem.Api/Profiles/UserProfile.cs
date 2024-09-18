using AutoMapper;
using ProjectManagementSystem.Api.DTOs.Tasks;
using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.Profiles
{
    public class USerProfile : Profile
    {
        public USerProfile() { 
            CreateMap<User, UserDto>();
        }
    }
}
