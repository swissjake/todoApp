using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Dto.Todo;
using TodoApp.Application.UseCaseInterface;

namespace TodoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var todos = await _todoService.GetAllTodosAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodo(int id)
    {
        try
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            return Ok(todo);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createTodoDto)
    {
        var createdTodo = await _todoService.CreateTodoAsync(createTodoDto);
        return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, createdTodo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto updateTodoDto)
    {
        updateTodoDto.Id = id;
        var updatedTodo = await _todoService.UpdateTodoAsync(updateTodoDto);
        return Ok(updatedTodo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        await _todoService.DeleteTodoAsync(id);
        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteTodo(int id)
    {
        await _todoService.CompleteTodoAsync(id);
        return Ok();
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompletedTodos()
    {
        var todos = await _todoService.GetAllCompletedAsync();
        return Ok(todos);
    }

    [HttpGet("incomplete")]
    public async Task<IActionResult> GetIncompleteTodos()
    {
        var todos = await _todoService.GetAllIncompleteAsync();
        return Ok(todos);
    }
}