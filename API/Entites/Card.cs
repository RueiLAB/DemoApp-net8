using System;

namespace API.Entities;

public class Card
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public ICollection<Todo> Todos { get; set; } = [];
}
