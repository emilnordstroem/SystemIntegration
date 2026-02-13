using Data.TodoAPI;
using Lektion3TodoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
	private readonly TodoContext _context;

	public TodoController(TodoContext context)
	{
		_context = context;
	}

	// GET: api/Todo
	[HttpGet]
	public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
	{
		return await _context.TodoItems.ToListAsync();
	}

	// POST: api/Todo
	[HttpPost]
	public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
	{
		_context.TodoItems.Add(todoItem);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
	}

	// GET: api/Todo/5
	[HttpGet("{id}")]
	public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
	{
		var todoItem = await _context.TodoItems.FindAsync(id);

		if (todoItem == null)
		{
			return NotFound();
		}

		return todoItem;
	}

	// PUT: api/Todo/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
	{
		if (id != todoItem.Id)
		{
			return BadRequest();
		}

		_context.Entry(todoItem).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!_context.TodoItems.Any(e => e.Id == id))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return NoContent();
	}

	// DELETE: api/Todo/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteTodoItem(long id)
	{
		var todoItem = await _context.TodoItems.FindAsync(id);
		if (todoItem == null)
		{
			return NotFound();
		}

		_context.TodoItems.Remove(todoItem);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
