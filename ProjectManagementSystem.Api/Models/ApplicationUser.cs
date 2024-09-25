using Microsoft.AspNetCore.Identity;
using ProjectManagementSystem.Api.Enums;


namespace ProjectManagementSystem.Api.Models;
public class ApplicationUser : IdentityUser
{
    public UserStatus Status { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
}