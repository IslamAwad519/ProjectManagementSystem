using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Api.Dtos.VerifyAccount
{
    public class VerifyOTPRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string OTP { get; set; }
    }
}
