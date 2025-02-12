using System;
using API.Entities;

namespace API.Repository.Interfaces;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetTodoListAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task AddAsync(Todo todo);
    void Update(Todo todo);
    void Delete(Todo todo);
    Task<bool> SaveChangesAsync();
}
