namespace ProjectManagementSystem.Api.Dtos.VerifyAccount
{
    public class VerifyOTPRequestDto
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
