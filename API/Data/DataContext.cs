using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Card> Cards { get; set; }
}
