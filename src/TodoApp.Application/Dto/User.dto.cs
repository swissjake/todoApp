using System;

namespace TodoApp.Application.Dto;

public class User.dto
{
    public int Id { get; set; }
public required string Name { get; set; }
public required string Email { get; set; }
public required string Password { get; set; }
public DateTime CreatedAt { get; set; }
public DateTime UpdatedAt { get; set; }
}
