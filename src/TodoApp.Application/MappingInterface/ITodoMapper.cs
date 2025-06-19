using System;
using TodoApp.Application.Dto.Todo;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.MappingInterface;

public interface ITodoMapper
{
    TodoDto MapToDto(Todo todo);
    Todo MapToEntity(CreateTodoDto createTodoDto);
    Todo MapToEntity(UpdateTodoDto updateTodoDto);

    CreateTodoDto MapToCreateTodoDto(Todo todo);
    UpdateTodoDto MapToUpdateTodoDto(Todo todo);
}
