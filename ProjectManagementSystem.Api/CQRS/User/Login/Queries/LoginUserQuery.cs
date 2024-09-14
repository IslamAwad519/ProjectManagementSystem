using MediatR;
using ProjectManagementSystem.Api.DTOs.Auth;

namespace ProjectManagementSystem.Api.CQRS.User.Login.Queries;

public class LoginUserQuery : IRequest<AuthResponse?>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
