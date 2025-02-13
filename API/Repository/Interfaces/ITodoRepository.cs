using System;
using API.Entities;

namespace API.Repository.Interfaces;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAllTodos();
    Task<Todo?> GetTodoById(int id);
    Task CreateTodo(Todo todo);
    void UpdateTodo(Todo todo);
    void DeleteTodo(Todo todo);
    void DeleteTodos(IEnumerable<Todo> todos);
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<Todo>> GetTodosByCardId(int cardId);
}
