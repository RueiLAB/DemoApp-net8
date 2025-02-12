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

    public async Task<IEnumerable<TodoDto>> GetTodoListAsync()
    {
        var todos = await _todoRepository.GetTodoListAsync();
        return todos.Select(todo => new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        });
    }

    public async Task<TodoDto?> GetTodoByIdAsync(int id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
        if (todo == null)
            return null;

        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public async Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto)
    {
        var todo = new Todo
        {
            Title = createTodoDto.Title,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        await _todoRepository.AddAsync(todo);
        await _todoRepository.SaveChangesAsync();

        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt
        };
    }

    public async Task<bool> UpdateTodoAsync(int id, UpdateTodoDto updateTodoDto)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
        if (todo == null)
            return false;

        todo.Title = updateTodoDto.Title;
        todo.IsCompleted = updateTodoDto.IsCompleted;
        todo.UpdatedAt = DateTime.UtcNow;

        _todoRepository.Update(todo);
        return await _todoRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
        if (todo == null)
            return false;

        _todoRepository.Delete(todo);
        return await _todoRepository.SaveChangesAsync();
    }
}
