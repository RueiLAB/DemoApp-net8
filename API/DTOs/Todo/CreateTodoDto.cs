using System;

namespace API.DTOs.Todo;

public class CreateTodoDto
{
    public int? CardId { get; set; }
    public required string Title { get; set; }
}
