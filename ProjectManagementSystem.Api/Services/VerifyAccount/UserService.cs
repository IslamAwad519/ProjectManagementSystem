using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.Models;
using static ProjectManagementSystem.Api.Services.VerifyAccount.UserService;

namespace ProjectManagementSystem.Api.Services.VerifyAccount
{
    public class UserService : IUserService
    {
            private readonly UserManager<ApplicationUser> _userManager;

            public UserService(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<bool> UserExistsAsync(string email)
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user != null;
            }
        }

    }

