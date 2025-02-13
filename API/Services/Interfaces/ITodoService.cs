using System;
using API.DTOs.Todo;
using API.Entities;


namespace API.Services.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoDto>> GetAllTodos();
    Task<TodoDto?> GetTodoById(int id);
    Task<TodoDto> CreateTodo(CreateTodoDto createTodoDto);
    Task<bool> UpdateTodo(int id, UpdateTodoDto updateTodoDto);
    Task<bool> DeleteTodo(int id);
    Task<IEnumerable<TodoDto>> GetTodosByCardId(int cardId);
    Task<bool> AssignTodosToCard(int cardId, List<int> todoIds);
}
