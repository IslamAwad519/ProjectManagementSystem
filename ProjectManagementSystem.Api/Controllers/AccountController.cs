using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.User.ChangePassword.Commands;
using ProjectManagementSystem.Api.CQRS.User.Login.Queries;
using ProjectManagementSystem.Api.CQRS.User.ResetPassword.Commands;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.GenerateOTP;
using ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.VerifyOTP;
using ProjectManagementSystem.Api.DTOs.ChangePassword;
using ProjectManagementSystem.Api.DTOs.ResetPassword;

namespace ProjectManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public AccountController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ResultViewModel<AuthResponse?>> Login([FromBody] LoginUserDto request)
    {
        var query = _mapper.Map<LoginUserQuery>(request);

        var result = await _mediator.Send(query);
    
        return new ResultViewModel<AuthResponse?>()
        {
            IsSuccess = result.IsSuccess,
            Data = result.Data,
            Message = result.Message
        };
    }
    
    [HttpPost("change-password")]
    public async Task<ResultViewModel<ChangePasswordResponse>> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var command = _mapper.Map<ChangePasswordCommand>(request);

        var result = await _mediator.Send(command);
        return new ResultViewModel<ChangePasswordResponse>()
        {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };
    }

    [HttpPost("forgot-password")]
    public async Task<ResultViewModel<ForgotPasswordResponseDto>> ForgotPassword(string email)
    {
        var command = new ForgotPasswordCommand  { Email = email };
        var result = await _mediator.Send(command);
        return new ResultViewModel<ForgotPasswordResponseDto>
        {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };
    }

    [HttpPost("generate-otp")]
    public async Task<ResultViewModel<GenerateOTPResponseDto>> GenerateOTP(string email)
    {
        var command = new GenerateOTPCommand { Email = email };
        var result = await _mediator.Send(command); 
        return new ResultViewModel<GenerateOTPResponseDto>()
        {
            IsSuccess = result.IsSuccess, 
            Message = result.Message     
        };
    }

    [HttpPost("verify-otp")]
    public async Task<ResultViewModel<VerifyOTPResponseDto>> VerifyOTP(VerifyOTPRequestDto otpVerificationRequest)
    {
       var command = _mapper.Map<VerifyOTPCommand>(otpVerificationRequest);
       var result = await _mediator.Send(command);
       return new ResultViewModel<VerifyOTPResponseDto>
       {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };
    
    }

    [HttpPost("ResetPassword")]
    public async Task<ResultViewModel<ResetPasswordToReturnDto>> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var command = _mapper.Map<ResetPasswordCommand>(request);
        var result = await _mediator.Send(command);

        return new ResultViewModel<ResetPasswordToReturnDto>()
        {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };
    }
}

