using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.Services.VerifyAccount;

namespace ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands
{
    public class GenerateOTPCommand : IRequest<GenerateOTPResponseDto>
    {
        public GenerateOTPRequestDto GenerateOTPRequestDto { get; set; }

        public GenerateOTPCommand(GenerateOTPRequestDto generateOTPRequestDto)
        {
            GenerateOTPRequestDto = generateOTPRequestDto;
        }
    }

    public class VerifyOTPCommand : IRequest<VerifyOTPResponseDto>
    {
        public string Email { get; set; }
        public string OTPCode { get; set; }
    }

    public class GenerateOTPCommandHandler : IRequestHandler<GenerateOTPCommand, GenerateOTPResponseDto>
    {
        private readonly IOTPService _otpService;

        public GenerateOTPCommandHandler(IOTPService otpService)
        {
            _otpService = otpService;
        }

        public async Task<GenerateOTPResponseDto> Handle(GenerateOTPCommand request, CancellationToken cancellationToken)
        {
            var otpCode = await _otpService.GenerateAndSendOTPAsync(request.GenerateOTPRequestDto.Email);

            return new GenerateOTPResponseDto
            {
                IsSuccess = true,
                Message = "OTP has been sent to your email."
            };
        }
    }

    public class VerifyOTPCommandHandler : IRequestHandler<VerifyOTPCommand, VerifyOTPResponseDto>
    {
        private readonly IOTPService _otpService;
        private readonly IUserService _userService; 

        public VerifyOTPCommandHandler(IOTPService otpService, IUserService userService)
        {
            _otpService = otpService;
            _userService = userService;
        }

        public async Task<VerifyOTPResponseDto> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            // Check if the user exists
            var userExists = await _userService.UserExistsAsync(request.Email);
            if (!userExists)
            {
                return new VerifyOTPResponseDto
                {
                    IsSuccess = false,
                    Message = "User does not exist"
                };
            }

            // Verify OTP
            var isValid = await _otpService.VerifyOTPAsync(request.Email, request.OTPCode);

            if (!isValid)
            {
                return new VerifyOTPResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid or expired OTP"
                };
            }

            return new VerifyOTPResponseDto
            {
                IsSuccess = true,
                Message = "OTP verified successfully"
            };
        }
    }

}

