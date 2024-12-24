using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Data;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoList()
        {
            var todoItems = await _context.TodoList.ToListAsync();
            return Ok(todoItems);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> AddTodoItem([FromBody] TodoItem todoItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.TodoList.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id)
        {
            var todoItem = await _context.TodoList.FindAsync(id);

            if (todoItem == null) return NotFound("TODO item not found");

            todoItem.Status = todoItem.Status switch
            {
                Status.pending => Status.completed,
                Status.completed => Status.pending,
                _ => todoItem.Status
            };

            await _context.SaveChangesAsync();
            return Ok(todoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoList.FindAsync(id);

            if (todoItem == null) return NotFound("TODO item not found");

            _context.TodoList.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
