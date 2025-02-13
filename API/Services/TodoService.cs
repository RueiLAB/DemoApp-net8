using API.DTOs.Todo;
using API.Entities;
using API.Repository.Interfaces;
using API.Services.Interfaces;

namespace API.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<TodoDto>> GetAllTodos()
    {
        var todos = await _todoRepository.GetAllTodos();

        var todoDtos = todos.Select(t => new TodoDto
        {
            Id = t.Id,
            Title = t.Title,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
            CardId = t.CardId
        });

        return todoDtos;
    }

    public async Task<TodoDto?> GetTodoById(int id)
    {
        var todo = await _todoRepository.GetTodoById(id);
        if (todo == null)
            return null;

        var todoDto = new TodoDto()
        {
            Id = todo.Id,
            Title = todo.Title,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt,
            CardId = todo.CardId
        };

        return todoDto;
    }

    public async Task<TodoDto> CreateTodo(CreateTodoDto createTodoDto)
    {
        var todo = new Todo()
        {
            Title = createTodoDto.Title,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            CardId = createTodoDto.CardId
        };

        await _todoRepository.CreateTodo(todo);
        await _todoRepository.SaveChangesAsync();

        var todoDto = new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt,
            CardId = todo.CardId
        };

        return todoDto;
    }

    public async Task<bool> UpdateTodo(int id, UpdateTodoDto updateTodoDto)
    {
        var todo = await _todoRepository.GetTodoById(id);
        if (todo == null) return false;

        todo.Title = updateTodoDto.Title;
        todo.IsCompleted = updateTodoDto.IsCompleted;
        todo.UpdatedAt = DateTime.UtcNow;

        _todoRepository.UpdateTodo(todo);
        return await _todoRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteTodo(int id)
    {
        var todo = await _todoRepository.GetTodoById(id);
        if (todo == null) return false;

        _todoRepository.DeleteTodo(todo);
        return await _todoRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<TodoDto>> GetTodosByCardId(int cardId)
    {
        var todos = await _todoRepository.GetTodosByCardId(cardId);

        var todoDtos = todos.Select(t => new TodoDto
        {
            Id = t.Id,
            Title = t.Title,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
            CardId = t.CardId
        });

        return todoDtos;
    }

    public async Task<bool> AssignTodosToCard(int cardId, List<int> todoIds)
    {
        // 檢查傳入的 TodoId 清單是否有資料
        if (todoIds == null || !todoIds.Any())
        {
            throw new ArgumentException("必須至少提供一筆 Todo 的 ID 進行指派。", nameof(todoIds));
        }

        foreach (var todoId in todoIds)
        {
            var todo = await _todoRepository.GetTodoById(todoId);
            if (todo == null)
            {
                // 如果其中一筆 Todo 找不到，就拋出例外
                throw new InvalidOperationException($"Todo ID 為 {todoId} 的項目不存在，無法進行指派。");
            }

            // 指派 Todo 到指定的 Card
            todo.CardId = cardId;
            _todoRepository.UpdateTodo(todo);
        }

        // 儲存所有變更
        return await _todoRepository.SaveChangesAsync();
    }
}
