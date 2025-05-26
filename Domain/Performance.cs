namespace Domain;

public class Performance
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? Duration { get; set; }

    public required string StageId { get; set; }
    public Stage? Stage { get; set; }

    public required string ProductionId { get; set; }
    public Production? Production { get; set; }
    public ICollection<UserPerformance> UserPerformances { get; set; } = [];
}
