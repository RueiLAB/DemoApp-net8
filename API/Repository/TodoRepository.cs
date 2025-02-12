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

    public async Task<IEnumerable<Todo>> GetTodoListAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<Todo?> GetByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task AddAsync(Todo todo)
    {
        await _context.Todos.AddAsync(todo);
    }

    public void Update(Todo todo)
    {
        _context.Todos.Update(todo);
    }

    public void Delete(Todo todo)
    {
        _context.Todos.Remove(todo);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
