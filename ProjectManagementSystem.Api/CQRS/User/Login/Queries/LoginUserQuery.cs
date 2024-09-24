using MediatR;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.User.Login.Queries;

public class LoginUserQuery : IRequest<Result<AuthResponse?>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
