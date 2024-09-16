namespace ProjectManagementSystem.Api.Models
{
    public class OTP
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
