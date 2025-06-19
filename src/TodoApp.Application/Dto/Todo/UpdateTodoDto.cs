using System;

namespace TodoApp.Application.Dto.Todo;

public class UpdateTodoDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}
