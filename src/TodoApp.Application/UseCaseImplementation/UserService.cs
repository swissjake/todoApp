using System;
using TodoApp.Application.Dto;
using TodoApp.Application.Dto.User;
using TodoApp.Application.MappingInterface;
using TodoApp.Application.UseCaseInterface;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.UseCaseImplementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public UserService(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(_userMapper.MapToDto);
    }

    // public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    // {
    //     var user = _userMapper.MapToEntity(createUserDto);
    //     var createdUser = await _userRepository.CreateUserAsync(user);
    //     return _userMapper.MapToDto(createdUser);
    // }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
            throw new ArgumentException($"User with ID {id} not found");

        return _userMapper.MapToDto(user);
    }

    public async Task<UserDto> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        var user = _userMapper.MapToEntity(updateUserDto);
        var updatedUser = await _userRepository.UpdateUserAsync(user);
        return _userMapper.MapToDto(updatedUser);
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
            throw new ArgumentException($"User with email {email} not found");
        return _userMapper.MapToDto(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
}
