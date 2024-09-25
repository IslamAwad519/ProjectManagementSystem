using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.Enums;


namespace ProjectManagementSystem.Api.Models;
public class ApplicationUser : IdentityUser
{
    //public string Country { get; set; }
    public UserStatus UserStatus { get; set; } = UserStatus.Active;

    public bool IsDeleted { get; set; }
}