using System;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Interfaces;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo?> GetTodoByIdAsync(int id);


    Task<Todo> AddAsync(Todo todo);
    Task<Todo> UpdateAsync(Todo todo);
    Task DeleteAsync(int id);

    Task<IEnumerable<Todo>> GetAllCompletedAsync();
    Task<IEnumerable<Todo>> GetAllIncompleteAsync();

    Task<Todo> CompleteTodoAsync(int id);

}
