using AutoMapper;
using ProjectManagementSystem.Api.CQRS.User.ChangePassword.Commands;
using ProjectManagementSystem.Api.CQRS.User.Login.Queries;
using ProjectManagementSystem.Api.CQRS.User.RestePassword.Commands;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.ViewModels.ForgetPassword;
using ProjectManagementSystem.Api.ViewModels.VerifyAccount;

namespace ProjectManagementSystem.Api.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ChangePasswordDto, ChangePasswordCommand>();

        CreateMap<LoginUserDto, LoginUserQuery>();

        CreateMap<ForgotPasswordRequestVM, ForgotPasswordRequestDto>();
        CreateMap<ForgotPasswordResponseDto, ForgotPasswordResponseVM>();
        CreateMap<GenerateOTPRequestVM, GenerateOTPRequestDto>();
        CreateMap<GenerateOTPResponseDto, OTPVerificationResponseVM>();
        CreateMap<OTPVerificationRequestVM, VerifyOTPRequestDto>();
        CreateMap<VerifyOTPResponseDto, OTPVerificationResponseVM>();
        CreateMap<ResetPasswordToReturnDto, ResetPasswordCommand>();
    }
}
