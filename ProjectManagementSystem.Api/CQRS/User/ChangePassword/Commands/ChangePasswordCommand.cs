using MediatR;

namespace ProjectManagementSystem.Api.CQRS.User.ChangePassword.Commands;

public class ChangePasswordCommand: IRequest<bool>
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
