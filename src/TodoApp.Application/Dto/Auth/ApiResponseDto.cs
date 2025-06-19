using System;
using TodoApp.Application.Dto.User;

namespace TodoApp.Application.Dto.Auth;

public class ApiResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAt { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
    public UserDto User { get; set; } = null!;

}
