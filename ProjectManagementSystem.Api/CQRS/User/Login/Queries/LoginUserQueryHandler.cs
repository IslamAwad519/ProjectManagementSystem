using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.DTOs.Auth;
using ProjectManagementSystem.Api.DTOs.Projects;
using ProjectManagementSystem.Api.Helpers.GenerateToken;
using ProjectManagementSystem.Api.Models;
using ProjectManagementSystem.Api.ViewModels.ResultViewModel;

namespace ProjectManagementSystem.Api.CQRS.User.Login.Queries;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<AuthResponse?>>
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

    public async Task<Result<AuthResponse?>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        // Check the user exists
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<AuthResponse?>.Failure($"user not found!");
        }

        // Check the password match the current user
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            return Result<AuthResponse?>.Failure($"error occured while sign in!!!");
        }
        //generate token 
        var role = await _userManager.GetRolesAsync(user);
        
        var (token, expiresIn) = _jwtGenerator.GenerateToken(user,role);

        var response = new AuthResponse(user.Id, user.Email, user.UserName!, token, expiresIn);
        // return response;
        return Result<AuthResponse?>.Success(response, "login successfully.");
    }
}
