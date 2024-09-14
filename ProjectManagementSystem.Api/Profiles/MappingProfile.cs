using AutoMapper;
using ProjectManagementSystem.Api.CQRS.User.ChangePassword.Queries;
using ProjectManagementSystem.Api.CQRS.User.Login.Queries;
using ProjectManagementSystem.Api.DTOs.Auth;

namespace ProjectManagementSystem.Api.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ChangePasswordDto, ChangePasswordQuery>();

        CreateMap<LoginUserDto, LoginUserQuery>();
    }
}
