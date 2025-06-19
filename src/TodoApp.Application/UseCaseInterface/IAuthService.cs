using System;
using TodoApp.Application.Dto.Auth;

namespace TodoApp.Application.UseCaseInterface;

public interface IAuthService
{
    Task<ApiResponseDto> LoginAsync(LoginDto loginDto);
    Task<ApiResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<ApiResponseDto> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync(string refreshToken);
}
