using MediatR;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;

namespace ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.VerifyOTP
{
    public class VerifyOTPCommand : IRequest<VerifyOTPResponseDto>
    {
        public string Email { get; set; }
        public string OTPCode { get; set; }
    }
}
