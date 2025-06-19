using TodoApp.Application.Dto.Todo;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.UseCaseInterface;

public interface ITodoService
{
    Task<IEnumerable<TodoDto>> GetAllTodosAsync();
    Task<TodoDto> GetTodoByIdAsync(int id);
    Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto);
    Task<TodoDto> UpdateTodoAsync(UpdateTodoDto updateTodoDto);
    Task DeleteTodoAsync(int id);
    Task<IEnumerable<TodoDto>> GetAllCompletedAsync();
    Task<IEnumerable<TodoDto>> GetAllIncompleteAsync();
    Task CompleteTodoAsync(int id);
}
