namespace API.DTO;

public class PerformanceSearchRequest
{
    public string[]? Dates { get; set; } = [];
    public string[]? Villages { get; set; } = [];
    public string[]? Stages { get; set; } = [];
    public string[]? Genres { get; set; } = [];
}
