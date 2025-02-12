using System;
using API.DTOs.Todo;
using API.Entities;


namespace API.Services.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoDto>> GetTodoListAsync();
    Task<TodoDto?> GetTodoByIdAsync(int id);
    Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto);
    Task<bool> UpdateTodoAsync(int id, UpdateTodoDto updateTodoDto);
    Task<bool> DeleteTodoAsync(int id);
}
