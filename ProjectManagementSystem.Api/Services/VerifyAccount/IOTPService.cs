namespace ProjectManagementSystem.Api.Services.VerifyAccount
{
    public interface IOTPService
    {
        Task<string> GenerateAndSendOTPAsync(string email);
        Task<bool> VerifyOTPAsync(string email, string otpCode);
    }
}
