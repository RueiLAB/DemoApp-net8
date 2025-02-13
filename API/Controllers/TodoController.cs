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

        /// <summary>
        /// 取得所有待辦事項
        /// </summary>
        /// <returns></returns>
        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetAllTodos()
        {
            var todos = await _todoService.GetAllTodos();

            return Ok(todos);
        }

        /// <summary>
        /// 取得待辦事項
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/todo/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoDto>> GetTodoById(int id)
        {
            var todo = await _todoService.GetTodoById(id);
            if (todo == null) return NotFound();

            return Ok(todo);
        }

        /// <summary>
        /// 新增待辦事項
        /// </summary>
        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] CreateTodoDto createTodoDto)
        {
            var todo = await _todoService.CreateTodo(createTodoDto);

            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        /// <summary>
        /// 修改待辦事項
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateTodoDto"></param>
        /// <returns></returns>
        // PUT: api/todo/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto updateTodoDto)
        {
            var result = await _todoService.UpdateTodo(id, updateTodoDto);
            if (!result) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// 刪除待辦事項
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/todo/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var result = await _todoService.DeleteTodo(id);

            if (!result) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// 取得特定卡片的所有待辦事項
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        // GET: api/todo/card/{cardId}
        [HttpGet("card/{cardId}")]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodosByCardId(int cardId)
        {
            var todos = await _todoService.GetTodosByCardId(cardId);
            return Ok(todos);
        }

        /// <summary>
        /// 指派待辦事項至指定卡片
        /// </summary>
        /// <param name="assignTodosDto"></param>
        /// <returns></returns>
        // PUT: api/todo/assign
        [HttpPut("assign")]
        public async Task<ActionResult> AssignTodosToCard([FromBody] AssignTodosDto assignTodosDto)
        {
            var result = await _todoService.AssignTodosToCard(assignTodosDto.CardId, assignTodosDto.TodoIds);
            if (!result) return NotFound("指派失敗,找不到資源");

            return NoContent();
        }
    }
}
