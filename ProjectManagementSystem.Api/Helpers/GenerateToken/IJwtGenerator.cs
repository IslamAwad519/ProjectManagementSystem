using ProjectManagementSystem.Api.Models;

namespace ProjectManagementSystem.Api.Helpers.GenerateToken;

public interface IJwtGenerator
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user, IList<string> roles);
}
