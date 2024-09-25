using AutoMapper;
using ProjectManagementSystem.Api.CQRS.User.ChangePassword.Commands;
using ProjectManagementSystem.Api.CQRS.User.Login.Queries;
using ProjectManagementSystem.Api.CQRS.User.ResetPassword.Commands;
using ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.VerifyOTP;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.DTOs.DashBoard;
using ProjectManagementSystem.Api.Models;


namespace ProjectManagementSystem.Api.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ChangePasswordDto, ChangePasswordCommand>();
        CreateMap<VerifyOTPRequestDto, VerifyOTPCommand>()
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.OTPCode, opt => opt.MapFrom(src => src.OTP));

        CreateMap<LoginUserDto, LoginUserQuery>();

        CreateMap<ResetPasswordRequest, ResetPasswordCommand>();
    }
}
