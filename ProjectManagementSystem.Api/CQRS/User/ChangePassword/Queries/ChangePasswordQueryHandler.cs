using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.Models;
using System.Security.Claims;

namespace ProjectManagementSystem.Api.CQRS.User.ChangePassword.Queries;

public class ChangePasswordQueryHandler : IRequestHandler<ChangePasswordQuery, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChangePasswordQueryHandler(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> Handle(ChangePasswordQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return false;
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        if (request.NewPassword != request.ConfirmPassword)
        {
            return false;
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            return false;
        }

        return true;
    }
}
