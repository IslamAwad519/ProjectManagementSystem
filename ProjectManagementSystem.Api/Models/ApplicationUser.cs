using Microsoft.AspNetCore.Identity;


namespace ProjectManagementSystem.Api.Models;
public class ApplicationUser : IdentityUser
{
    //public string Country { get; set; }
    public bool IsDeleted { get; set; }
}