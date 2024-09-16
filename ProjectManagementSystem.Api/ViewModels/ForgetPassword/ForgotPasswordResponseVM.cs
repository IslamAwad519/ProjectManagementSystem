namespace ProjectManagementSystem.Api.ViewModels.ForgetPassword
{
    public class ForgotPasswordResponseVM
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ResetLink { get; set; }
    }
}
