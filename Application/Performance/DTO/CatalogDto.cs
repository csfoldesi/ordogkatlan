namespace Application.Performance.DTO;

public class CatalogDto
{
    public List<VillageDto> Villages { get; set; } = [];
    public List<StageDto> Stages { get; set; } = [];
    public List<GenreDto> Genres { get; set; } = [];
    public List<DateTime> Dates { get; set; } = [];
}
