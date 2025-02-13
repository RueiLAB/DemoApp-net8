using System;

namespace API.DTOs.Todo;

public class AssignTodosDto
{
    public int CardId { get; set; }
    public List<int> TodoIds { get; set; } = new List<int>();
}
