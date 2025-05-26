namespace Domain;

public class UserPerformance
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string UserId { get; set; }
    public User? User { get; set; }

    public required Guid PerformanceId { get; set; }
    public Performance? Performance { get; set; }
}
