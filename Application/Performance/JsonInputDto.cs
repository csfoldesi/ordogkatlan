namespace Application.Performance.Json;

public class JsonInputRoot
{
    public required Data Data { get; set; }
    public string[] Display { get; set; } = [];
    public Seo? Seo { get; set; }
    public string? RawSeo { get; set; }
    public object[]? Lightbox { get; set; }
}

public class Data
{
    public required Controls Controls { get; set; }
    public Centerpiece[] Centerpiece { get; set; } = [];
    public string? prequel { get; set; }
    public string? sequel { get; set; }
}

public class Controls
{
    public Day[] days { get; set; } = [];
    public Time? time { get; set; }
    public Village[] Villages { get; set; } = [];
    public object[] stages { get; set; } = [];
    public Genre[] Genres { get; set; } = [];
}

public class Time
{
    public string[] values { get; set; } = [];
    public string? selected { get; set; }
}

public class Day
{
    public required string id { get; set; }
    public required string name { get; set; }
    public bool? selected { get; set; }
}

public class Village
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public bool? Selected { get; set; }
}

public class Genre
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public Featuredcolor? FeaturedColor { get; set; }
    public bool? Selected { get; set; }
}

public class Featuredcolor
{
    public string? background { get; set; }
    public string? foreground { get; set; }
}

public class Centerpiece
{
    public required string ProductionId { get; set; }
    public required string Title { get; set; }
    public Thumbnail? Thumbnail { get; set; }
    public required Village1 Village { get; set; }
    public required Stage Stage { get; set; }
    public required Time1 Time { get; set; }
    public int? Duration { get; set; }
    public string? Description { get; set; }
    public string? Href { get; set; }
    public Label[] Labels { get; set; } = [];
    public Ticketed? Ticketed { get; set; }
}

public class Thumbnail
{
    public string? Mobile { get; set; }
    public string? Web { get; set; }
}

public class Village1
{
    public string? Href { get; set; }
    public string? Name { get; set; }
}

public class Stage
{
    public string? Href { get; set; }
    public string? Name { get; set; }
}

public class Time1
{
    public string? Href { get; set; }
    public string? Name { get; set; }
}

public class Ticketed
{
    public string? Href { get; set; }
    public string? Name { get; set; }
}

public class Label
{
    public string? Href { get; set; }
    public string? Name { get; set; }
    public Featuredcolor1? FeaturedColor { get; set; }
}

public class Featuredcolor1
{
    public string? Background { get; set; }
    public string? Foreground { get; set; }
}

public class Seo
{
    public string? title { get; set; }
    public object[] link { get; set; } = [];
    public Meta[] meta { get; set; } = [];
}

public class Meta
{
    public string? property { get; set; }
    public string? content { get; set; }
    public string? name { get; set; }
}
