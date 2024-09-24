using MediatR;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;
using ProjectManagementSystem.Api.Services.IOTPService;

namespace ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.GenerateOTP
{
    public class GenerateOTPCommandHandler : IRequestHandler<GenerateOTPCommand, GenerateOTPResponseDto>
    {
        private readonly IOTPService _otpService;

        public GenerateOTPCommandHandler(IOTPService otpService)
        {
            _otpService = otpService;
        }

        public async Task<GenerateOTPResponseDto> Handle(GenerateOTPCommand request, CancellationToken cancellationToken)
        {
            var otpCode = await _otpService.GenerateAndSendOTPAsync(request.Email);

            return new GenerateOTPResponseDto
            {
                IsSuccess = true,
                Message = "OTP has been sent to your email."
            };
        }
    }
}
