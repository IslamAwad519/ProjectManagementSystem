using MediatR;

namespace ProjectManagementSystem.Api.CQRS.User.ChangePassword.Queries;

public class ChangePasswordQuery: IRequest<bool>
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
