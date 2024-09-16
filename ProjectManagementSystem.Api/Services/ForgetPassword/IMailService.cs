using ProjectManagementSystem.Api.Services.ForgetPassword;
namespace ProjectManagementSystem.Api.Services.ForgetPassword
{
    public interface IMailService
    {
        Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile>? attachments = null);
    }
}
