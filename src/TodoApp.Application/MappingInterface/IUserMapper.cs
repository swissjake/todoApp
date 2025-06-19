using System;
using TodoApp.Application.Dto;
using TodoApp.Application.Dto.User;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.MappingInterface;

public interface IUserMapper
{
    UserDto MapToDto(User user);
    User MapToEntity(CreateUserDto createUserDto);
    User MapToEntity(UpdateUserDto updateUserDto);

    CreateUserDto MapToCreateUserDto(User user);
    UpdateUserDto MapToUpdateUserDto(User user);
}
