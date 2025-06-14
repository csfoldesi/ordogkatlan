﻿namespace Domain;

public class Village
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Style { get; set; }

    public ICollection<Stage> Stages { get; set; } = [];
}
