using System;
using TodoApp.Application.Dto.Todo;
using TodoApp.Application.MappingInterface;
using TodoApp.Application.UseCaseInterface;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.UseCaseImplementation;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly ITodoMapper _todoMapper;

    public TodoService(ITodoRepository todoRepository, ITodoMapper todoMapper)
    {
        _todoRepository = todoRepository;
        _todoMapper = todoMapper;
    }

    public async Task<IEnumerable<TodoDto>> GetAllTodosAsync()
    {
        var todos = await _todoRepository.GetAllAsync();
        return todos.Select(_todoMapper.MapToDto);
    }

    public async Task<TodoDto> GetTodoByIdAsync(int id)
    {
        var todo = await _todoRepository.GetTodoByIdAsync(id);

        if (todo == null)
            throw new ArgumentException($"Todo with ID {id} not found");

        return _todoMapper.MapToDto(todo);
    }

    public async Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto)
    {
        var todo = _todoMapper.MapToEntity(createTodoDto);
        var createdTodo = await _todoRepository.AddAsync(todo);
        return _todoMapper.MapToDto(createdTodo);
    }

    public async Task<TodoDto> UpdateTodoAsync(UpdateTodoDto updateTodoDto)
    {
        var todo = _todoMapper.MapToEntity(updateTodoDto);
        var updatedTodo = await _todoRepository.UpdateAsync(todo);
        return _todoMapper.MapToDto(updatedTodo);
    }

    public async Task DeleteTodoAsync(int id)
    {
        await _todoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<TodoDto>> GetAllCompletedAsync()
    {
        var todos = await _todoRepository.GetAllCompletedAsync();
        return todos.Select(_todoMapper.MapToDto);
    }

    public async Task<IEnumerable<TodoDto>> GetAllIncompleteAsync()
    {
        var todos = await _todoRepository.GetAllIncompleteAsync();
        return todos.Select(_todoMapper.MapToDto);
    }


}
