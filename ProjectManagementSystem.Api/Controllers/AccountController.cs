using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.User.ChangePassword.Commands;
using ProjectManagementSystem.Api.CQRS.User.Login.Queries;
using ProjectManagementSystem.Api.CQRS.User.RestePassword.Commands;
using ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.Exceptions.Error;
using ProjectManagementSystem.Api.Helpers;

using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.GenerateOTP;
using ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.VerifyOTP;

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
    public async Task<IActionResult> Login([FromBody] LoginUserDto request)
    {
        var query = _mapper.Map<LoginUserQuery>(request);

        var result = await _mediator.Send(query);
        if (result is null)
        {
            return Unauthorized("Password or username is wrong");
        }

        return Ok(result);
    }
    //[Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var command = _mapper.Map<ChangePasswordCommand>(request);

        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok("Password changed successfully");
        }
        return BadRequest("Something went wrong !");
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
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordToReturnDto request)
    {
        var Command = _mapper.Map<ResetPasswordCommand>(request);
        var result = await _mediator.Send(Command);

        return Ok(result);
    }
}

