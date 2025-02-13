using System;
using API.Data;
using API.Entities;
using API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly DataContext _context;
    public TodoRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Todo>> GetAllTodos()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<Todo?> GetTodoById(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task CreateTodo(Todo todo)
    {
        await _context.Todos.AddAsync(todo);
    }

    public void UpdateTodo(Todo todo)
    {
        _context.Todos.Update(todo);
    }

    public void DeleteTodo(Todo todo)
    {
        _context.Todos.Remove(todo);
    }

    public void DeleteTodos(IEnumerable<Todo> todos)
    {
        _context.Todos.RemoveRange(todos);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Todo>> GetTodosByCardId(int cardId)
    {
        return await _context.Todos
                             .Where(t => t.CardId == cardId)
                             .ToListAsync();
    }
}
