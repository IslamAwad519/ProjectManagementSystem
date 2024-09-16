using AutoMapper;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.ViewModels.ForgetPassword;
using ProjectManagementSystem.Api.Profiles;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.ViewModels.VerifyAccount;

namespace ProjectManagementSystem.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ForgotPasswordRequestVM, ForgotPasswordRequestDto>();
            CreateMap<ForgotPasswordResponseDto, ForgotPasswordResponseVM>();
            CreateMap<GenerateOTPRequestVM, GenerateOTPRequestDto>();
            CreateMap<GenerateOTPResponseDto, OTPVerificationResponseVM>();
            CreateMap<OTPVerificationRequestVM, VerifyOTPRequestDto>();
            CreateMap<VerifyOTPResponseDto, OTPVerificationResponseVM>();


        }
    }
}
