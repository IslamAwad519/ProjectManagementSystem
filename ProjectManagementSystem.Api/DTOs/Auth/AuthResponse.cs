namespace ProjectManagementSystem.Api.DTOs.Auth;

public record AuthResponse(
    string Id,
    string? Email,
    string UserName,
    string Token,
    int ExpiresIn
);
