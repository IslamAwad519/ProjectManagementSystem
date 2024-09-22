using MediatR;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.Services.VerifyAccount;

namespace ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.VerifyOTP
{
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
