using MediatR;
using ProjectManagementSystem.Api.Dtos.VerifyAccount;

namespace ProjectManagementSystem.Api.CQRS.User.VerifyAccount.Commands.GenerateOTP
{
    public class GenerateOTPCommand : IRequest<GenerateOTPResponseDto>
    {
        public string Email { get; set; }
    }
}
