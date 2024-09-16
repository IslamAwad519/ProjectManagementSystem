using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.ViewModels.VerifyAccount
{
    public class OTPVerificationRequestVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string OTP { get; set; }
    }
}
