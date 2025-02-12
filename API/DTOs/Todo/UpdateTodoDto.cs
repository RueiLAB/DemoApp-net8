using System;

namespace API.DTOs.Todo;

public class UpdateTodoDto
{
    public required string Title { get; set; }
    public bool IsCompleted { get; set; }
}
