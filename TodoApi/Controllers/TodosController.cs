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

    public TodosController(ITodoData data)
    {
        _data = data;
    }
    private int GetUserId()
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userId = int.Parse(userIdText);
        return userId;
    }

    // GET: api/Todos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoModel>>> Get()
    {
        var userId = GetUserId();

        var output = await _data.GetAllAssigned(userId);

        return Ok(output);
    }

    // GET api/Todos/5
    [HttpGet("{todoId}")]
    public async Task<ActionResult<TodoModel>> Get(int todoId)
    {
        var userId = GetUserId();

        var output = await _data.GetOneAssigned(userId, todoId);

        return Ok(output);
    }

    // POST api/Todos
    [HttpPost]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
    {
        var userId = GetUserId();

        var output = await _data.Create(userId, task);

        return Ok(output);
    }

    // PUT api/Todos/5
    [HttpPut("{todoId}")]
    public async Task<IActionResult> Put(int todoId, [FromBody] string task)
    {
        var userId = GetUserId();

        await _data.UpdateTask(userId, todoId, task);

        return Ok();
    }

    // PUT api/Todos/5/Complete
    [HttpPut("{todoId}/Complete")]
    public async Task<IActionResult> Complete(int todoId)
    {
        var userId = GetUserId();

        await _data.CompleteToDo(userId, todoId);

        return Ok();
    }

    // DELETE api/Todos/5
    [HttpDelete("{todoId}")]
    public async Task<IActionResult> Delete(int todoId)
    {
        var userId = GetUserId();

        await _data.Delete(userId, todoId);

        return Ok();
    }

}
