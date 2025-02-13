using System;
using API.DTOs.Todo;

namespace API.DTOs.Card;

public class CardDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public IEnumerable<TodoDto> Todos { get; set; } = [];
}
