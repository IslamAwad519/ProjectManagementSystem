using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.Helpers;
using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.Data.SeedData;

public static class DbInitializer
{
    public static async Task Initialize(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext context)
    {
        if (!await roleManager.RoleExistsAsync(AppRoles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Manager));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.User));

            var admin = new ApplicationUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
                PhoneNumber = "01004255366",
            };
            await userManager.CreateAsync(admin, "Password1!");
            await userManager.AddToRoleAsync(admin, AppRoles.Admin);
        }
    }
}