using AutoMapper;
using ProjectManagementSystem.Api.CQRS.User.ChangePassword.Commands;
using ProjectManagementSystem.Api.CQRS.User.Login.Queries;
using ProjectManagementSystem.Api.CQRS.User.Register.Commands;
using ProjectManagementSystem.Api.CQRS.User.RestePassword.Commands;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.DTOs.ResultDTO;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.ViewModels.ForgetPassword;
using ProjectManagementSystem.Api.ViewModels.RegisterUserVM;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
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
        CreateMap<RegisterUserVM, RegisterUserRequestDTO>().ReverseMap();

        CreateMap<ResultDTO, ResultViewModel<object>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.IsSuccess))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
            .ForMember(dest => dest.ErrorCode, opt => opt.MapFrom(src => src.IsSuccess ? ErrorCode.NoError : ErrorCode.UnKnown));

        CreateMap<ResultViewModel<dynamic>, ResultDTO>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.IsSuccess))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
    }
}
