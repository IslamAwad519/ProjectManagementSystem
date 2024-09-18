namespace ProjectManagementSystem.Api.DTOs.Auth
{
    public class ResetPasswordToReturnDto
    {
        public string Email { get; set; }
        public string newPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
