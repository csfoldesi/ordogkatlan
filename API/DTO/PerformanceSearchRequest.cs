using Application.Core;

namespace API.DTO;

public class PerformanceSearchRequest : PagedQuery
{
    public string[]? Dates { get; set; } = [];
    public string[]? Villages { get; set; } = [];
    public string[]? Stages { get; set; } = [];
    public string[]? Genres { get; set; } = [];
}
