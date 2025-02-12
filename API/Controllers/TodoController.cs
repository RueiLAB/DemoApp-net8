using API.DTOs.Todo;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodoController : BaseApiController
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodoList()
        {
            var todos = await _todoService.GetTodoListAsync();

            return Ok(todos);
        }

        // GET: api/todo/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoDto>> GetTodoById(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();

            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] CreateTodoDto createTodoDto)
        {
            var todo = await _todoService.CreateTodoAsync(createTodoDto);

            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        // PUT: api/todo/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto updateTodoDto)
        {
            var result = await _todoService.UpdateTodoAsync(id, updateTodoDto);
            if (!result) return NotFound();

            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var result = await _todoService.DeleteTodoAsync(id);

            if (!result) return NotFound();

            return NoContent();
        }
        
    }
}
