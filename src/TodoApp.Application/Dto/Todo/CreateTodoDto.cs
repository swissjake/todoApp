using System;

namespace TodoApp.Application.Dto.Todo;

public class CreateTodoDto
{

    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}
