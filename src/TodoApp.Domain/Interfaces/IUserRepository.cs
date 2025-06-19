
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(int id);
    Task<User> UpdateUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task DeleteUserAsync(int id);
}
