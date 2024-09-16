namespace ProjectManagementSystem.Api.Services.VerifyAccount
{
    public interface IUserService
    {
        Task<bool> UserExistsAsync(string email);
    }
}
