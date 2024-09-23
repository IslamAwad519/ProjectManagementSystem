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
using ProjectManagementSystem.Api.ViewModels.ForgetPassword;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;
using ProjectManagementSystem.Api.ViewModels.VerifyAccount;

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
    public async Task<ResultViewModel<ForgotPasswordResponseVM>> ForgotPassword([FromForm] ForgotPasswordRequestVM forgotPasswordRequestVM)
    {
        var forgotPasswordRequestDto = forgotPasswordRequestVM.MapOne<ForgotPasswordRequestDto>();
        var command = new ForgotPasswordCommand(forgotPasswordRequestDto);
        var resultDto = await _mediator.Send(command);
        var responseVM = resultDto.MapOne<ForgotPasswordResponseVM>();
        if (!responseVM.IsSuccess)
        {
            return ResultViewModel<ForgotPasswordResponseVM>.Failure(ErrorCode.ValidationError, responseVM.Message);
        }
        return ResultViewModel<ForgotPasswordResponseVM>.Success(responseVM, responseVM.Message);
    }
    [HttpPost("generate-otp")]
    public async Task<ResultViewModel<OTPVerificationResponseVM>> GenerateOTP([FromForm] GenerateOTPRequestVM generateOTPRequestVM)
    {
        var generateOtpRequestDto = generateOTPRequestVM.MapOne<GenerateOTPRequestDto>();
        var command = new GenerateOTPCommand(generateOtpRequestDto);
        var resultDto = await _mediator.Send(command);
        var responseVM = resultDto.MapOne<OTPVerificationResponseVM>();

        if (!responseVM.IsSuccess)
        {
            return ResultViewModel<OTPVerificationResponseVM>.Failure(ErrorCode.ValidationError, responseVM.Message);
        }
        return ResultViewModel<OTPVerificationResponseVM>.Success(responseVM, responseVM.Message);
    }
    [HttpPost("verify-otp")]
    public async Task<ResultViewModel<OTPVerificationResponseVM>> VerifyOTP([FromForm] OTPVerificationRequestVM otpVerificationRequestVM)
    {
        var verifyOtpRequestDto = otpVerificationRequestVM.MapOne<VerifyOTPRequestDto>();
        var command = new VerifyOTPCommand { Email = verifyOtpRequestDto.Email, OTPCode = verifyOtpRequestDto.OTP };
        var resultDto = await _mediator.Send(command);
        var responseVM = resultDto.MapOne<OTPVerificationResponseVM>();
        if (!responseVM.IsSuccess)
        {
            return ResultViewModel<OTPVerificationResponseVM>.Failure(ErrorCode.ValidationError, responseVM.Message);
        }
        return ResultViewModel<OTPVerificationResponseVM>.Success(responseVM, responseVM.Message);
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordToReturnDto request)
    {
        var Command = _mapper.Map<ResetPasswordCommand>(request);
        var result = await _mediator.Send(Command);

        return Ok(result);
    }

    


}

