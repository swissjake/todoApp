using System;
using TodoApp.Application.Dto.Auth;
using TodoApp.Application.Dto.User;
using TodoApp.Application.MappingInterface;
using TodoApp.Application.UseCaseInterface;
using TodoApp.Application.Services;
using TodoApp.Domain.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.UseCaseImplementation;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserMapper _userMapper;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IUserMapper userMapper,
        IJwtService jwtService,
        IPasswordService passwordService)

    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _userMapper = userMapper;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    public async Task<ApiResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Check if user already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists");

        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = _passwordService.HashPassword(registerDto.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdUser = await _userRepository.CreateUserAsync(user);

        // Return user info without tokens
        return new ApiResponseDto
        {
            AccessToken = string.Empty,
            RefreshToken = string.Empty,
            AccessTokenExpiresAt = DateTime.UtcNow,
            RefreshTokenExpiresAt = DateTime.UtcNow,
            User = _userMapper.MapToDto(createdUser)
        };
    }

    public async Task<ApiResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
        if (user == null || !_passwordService.VerifyPassword(loginDto.Password, user.Password))
            throw new UnauthorizedAccessException("Invalid credentials");

        await _refreshTokenRepository.RevokeAllUserTokensAsync(user.Id);

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Save new refresh token
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            ExpiresAt = _jwtService.GetRefreshTokenExpiration(),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false,
            UserId = user.Id
        };

        await _refreshTokenRepository.CreateAsync(refreshTokenEntity);

        return new ApiResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = _jwtService.GetAccessTokenExpiration(),
            RefreshTokenExpiresAt = _jwtService.GetRefreshTokenExpiration(),
            User = _userMapper.MapToDto(user)
        };
    }

    public async Task<ApiResponseDto> RefreshTokenAsync(string refreshToken)
    {
        // Get refresh token from database (no JWT validation needed for random string tokens)
        var refreshTokenEntity = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (refreshTokenEntity == null)
            throw new UnauthorizedAccessException("Refresh token not found in database");

        if (refreshTokenEntity.IsRevoked)
            throw new UnauthorizedAccessException("Refresh token has been revoked");

        if (refreshTokenEntity.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token has expired");

        // Get user
        var user = await _userRepository.GetUserByIdAsync(refreshTokenEntity.UserId);
        if (user == null)
            throw new UnauthorizedAccessException("User not found");

        // Revoke old refresh token
        await _refreshTokenRepository.RevokeTokenAsync(refreshToken);

        // Generate new tokens
        var newAccessToken = _jwtService.GenerateAccessToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        // Save new refresh token
        var newRefreshTokenEntity = new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresAt = _jwtService.GetRefreshTokenExpiration(),
            CreatedAt = DateTime.UtcNow,
            IsRevoked = false,
            UserId = user.Id
        };

        await _refreshTokenRepository.CreateAsync(newRefreshTokenEntity);

        return new ApiResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            AccessTokenExpiresAt = _jwtService.GetAccessTokenExpiration(),
            RefreshTokenExpiresAt = _jwtService.GetRefreshTokenExpiration(),
            User = _userMapper.MapToDto(user)
        };
    }


    public async Task LogoutAsync(string refreshToken)
    {
        await _refreshTokenRepository.RevokeTokenAsync(refreshToken);
    }
}