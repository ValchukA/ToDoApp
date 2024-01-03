﻿namespace ToDoApp.Bll.Features.Tasks;

public record TaskModel
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public required string CreatedBy { get; init; }
}
