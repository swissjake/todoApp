using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    bool ValidateAccessToken(string token);
    bool ValidateRefreshToken(string token);
    string GetUserIdFromToken(string token);
    DateTime GetAccessTokenExpiration();
    DateTime GetRefreshTokenExpiration();
}