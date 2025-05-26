namespace Domain;

public class Production
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
    public bool IsTicketed { get; set; }

    public List<Performance> Performances { get; set; } = [];
    public List<Genre> Genres { get; set; } = [];
}
