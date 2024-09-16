using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.ViewModels.VerifyAccount
{
    public class GenerateOTPRequestVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
