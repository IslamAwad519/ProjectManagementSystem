using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.ViewModels.ForgetPassword
{
    public class ForgotPasswordRequestVM
    {
        [Required]
        public string Email { get; set; }
    }
}
