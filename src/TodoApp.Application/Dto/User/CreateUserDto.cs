using System;

namespace TodoApp.Application.Dto;

public class CreateUserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
