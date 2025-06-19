using System;
using TodoApp.Application.Dto;
using TodoApp.Application.Dto.User;
using TodoApp.Application.MappingInterface;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.MappingImplementaton;

public class UserMapper : IUserMapper
{
    public UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public User MapToEntity(UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            CreatedAt = userDto.CreatedAt,
            UpdatedAt = userDto.UpdatedAt
        };
    }

    public CreateUserDto MapToCreateUserDto(User user)
    {
        return new CreateUserDto
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }

    public User MapToEntity(CreateUserDto createUserDto)
    {
        return new User
        {

            Name = createUserDto.Name,
            Email = createUserDto.Email,
            Password = createUserDto.Password,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public User MapToEntity(UpdateUserDto updateUserDto)
    {
        return new User
        {
            Name = updateUserDto.Name,
            Email = updateUserDto.Email,
            Password = updateUserDto.Password,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public UpdateUserDto MapToUpdateUserDto(User user)
    {
        return new UpdateUserDto
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }
}


