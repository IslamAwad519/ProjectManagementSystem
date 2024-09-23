using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.ViewModels.RegisterUserVM
{
    public class RegisterUserVM
    {
 
        public string UserName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        
        public string Country { get; set; }
    }
}
