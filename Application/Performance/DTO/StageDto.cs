﻿namespace Application.Performance.DTO;

public class StageDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string VillageId { get; set; }
    public string? Style { get; set; }
}
