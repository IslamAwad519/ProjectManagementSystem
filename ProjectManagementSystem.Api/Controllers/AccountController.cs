using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands;
using ProjectManagementSystem.Api.Dtos.ForgetPassword;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
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

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
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

}

