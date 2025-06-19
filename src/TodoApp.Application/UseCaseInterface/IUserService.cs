
using TodoApp.Application.Dto;
using TodoApp.Application.Dto.User;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.UseCaseInterface;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> UpdateUserAsync(UpdateUserDto updateUserDto);
    Task DeleteUserAsync(int id);

}
