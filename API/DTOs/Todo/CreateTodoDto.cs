using System;

namespace API.DTOs.Todo;

public class CreateTodoDto
{
    public required string Title { get; set; }
}
