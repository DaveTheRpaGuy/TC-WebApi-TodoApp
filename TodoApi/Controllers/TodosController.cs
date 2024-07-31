using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoLibrary.DataAccess;
using TodoLibrary.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoData _data;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoData data, ILogger<TodosController> logger)
    {
        _data = data;
        _logger = logger;
    }
    private int GetUserId()
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userId = int.Parse(userIdText);
        return userId;
    }

    // GET: api/Todos
    [HttpGet(Name = "GetAllTodos")]
    public async Task<ActionResult<IEnumerable<TodoModel>>> Get()
    {
        _logger.LogInformation("GET: api/Todos");

        try
        {
            var userId = GetUserId();

            var output = await _data.GetAllAssigned(userId);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to api/Todos failed.");
            return BadRequest();
        }

        
    }

    // GET api/Todos/5
    [HttpGet("{todoId}", Name = "GetOneTodo")]
    public async Task<ActionResult<TodoModel>> Get(int todoId)
    {
        _logger.LogInformation("GET: api/Todos/{TodoId}", todoId);
        try
        {
            var userId = GetUserId();

            var output = await _data.GetOneAssigned(userId, todoId);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to {ApiPath} failed. The Id was {TodoId}.",
                $"api/Todos/Id",
                todoId);
            return BadRequest();
        }
    }

    // POST api/Todos
    [HttpPost(Name = "CreateTodo")]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
    {
        _logger.LogInformation("POST: api/Todos (Task: {Task})", task);
        try
        {
            var userId = GetUserId();

            var output = await _data.Create(userId, task);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The POST call to {ApiPath} failed.",
                $"api/Todos/Id");
            return BadRequest();
        }
    }

    // PUT api/Todos/5
    [HttpPut("{todoId}", Name = "UpdateTodoTask")]
    public async Task<IActionResult> Put(int todoId, [FromBody] string task)
    {
        _logger.LogInformation("PUT: api/Todos/{TodoId} (Task: {Task})", todoId, task);
        try
        {
            var userId = GetUserId();

            await _data.UpdateTask(userId, todoId, task);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The PUT call to {ApiPath} failed. The Id was {TodoId}. (Task: {Task})",
                $"api/Todos/Id",
                todoId,
                task);
            return BadRequest();
        }
    }

    // PUT api/Todos/5/Complete
    [HttpPut("{todoId}/Complete", Name = "CompleteToDo")]
    public async Task<IActionResult> Complete(int todoId)
    {
        _logger.LogInformation("PUT: api/Todos/{TodoId}/Complete", todoId);
        try
        {
            var userId = GetUserId();

            await _data.CompleteToDo(userId, todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The PUT call to {ApiPath} failed. The Id was {TodoId}.",
                $"api/Todos/Id/Complete",
                todoId);
            return BadRequest();
        }
    }

    // DELETE api/Todos/5
    [HttpDelete("{todoId}", Name = "DeleteTodo")]
    public async Task<IActionResult> Delete(int todoId)
    {
        _logger.LogInformation("DELETE: api/Todos/{TodoId}", todoId);
        try
        {
            var userId = GetUserId();

            await _data.Delete(userId, todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The DELETE call to {ApiPath} failed. The Id was {TodoId}.",
                $"api/Todos/Id",
                todoId);
            return BadRequest();
        }
    }

}
