using System;
using TodoApp.Application.Dto.Todo;
using TodoApp.Application.MappingInterface;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.MappingImplementaton;

public class TodoMapper : ITodoMapper
{
    public TodoDto MapToDto(Todo todo)
    {
        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public Todo MapToEntity(TodoDto todoDto)
    {
        return new Todo
        {
            Id = todoDto.Id,
            Title = todoDto.Title,
            Description = todoDto.Description,
            IsCompleted = todoDto.IsCompleted,
            CreatedAt = todoDto.CreatedAt,
            UpdatedAt = todoDto.UpdatedAt
        };
    }

    public CreateTodoDto MapToCreateTodoDto(Todo todo)
    {
        return new CreateTodoDto
        {
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted
        };
    }

    public UpdateTodoDto MapToUpdateTodoDto(Todo todo)
    {
        return new UpdateTodoDto
        {
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted
        };
    }

    public Todo MapToEntity(CreateTodoDto createTodoDto)
    {
        return new Todo
        {
            Title = createTodoDto.Title,
            Description = createTodoDto.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public Todo MapToEntity(UpdateTodoDto updateTodoDto)
    {
        return new Todo
        {
            Title = updateTodoDto.Title,
            Description = updateTodoDto.Description,
            IsCompleted = updateTodoDto.IsCompleted ?? false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
