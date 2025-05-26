namespace Domain;

public class Genre
{
    public required string Id { get; set; }
    public required string Name { get; set; }

    public List<Production> Productions { get; set; } = [];
}
