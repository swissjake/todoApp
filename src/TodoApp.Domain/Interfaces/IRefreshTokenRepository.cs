using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId);
    Task RevokeTokenAsync(string token);
    Task RevokeAllUserTokensAsync(int userId);
}