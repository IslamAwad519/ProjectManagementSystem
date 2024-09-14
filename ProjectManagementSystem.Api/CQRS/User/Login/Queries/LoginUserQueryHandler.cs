using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.Helpers.GenerateToken;
using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.CQRS.User.Login.Queries;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthResponse?>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginUserQueryHandler(
        SignInManager<ApplicationUser> signInManager, 
        UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<AuthResponse?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        // Check the user exists
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return null;
        }

        // Check the password match the current user
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            return null;
        }

        //generate token 
        var (token, expiresIn) = _jwtGenerator.GenerateToken(user);
        var response = new AuthResponse(user.Id, user.Email, user.UserName!, token, expiresIn);
        return response;
    }
}
