using Application.Performance.DTO;
using AutoMapper;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Village, VillageDto>();
        CreateMap<Domain.Stage, StageDto>()
            .ForMember(dest => dest.Style, opt => opt.MapFrom(src => src.Village!.Style));
        CreateMap<Domain.Genre, GenreDto>();

        CreateMap<Domain.Performance, PerformanceDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.ProductionId))
            .ForMember(d => d.PerformanceId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Date, o => o.MapFrom(s => s.StartTime!.Value.Date))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Production!.Title))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Production!.Description))
            .ForMember(d => d.Thumbnail, o => o.MapFrom(s => s.Production!.Thumbnail))
            .ForMember(d => d.StageId, o => o.MapFrom(s => s.StageId))
            .ForMember(d => d.StageName, o => o.MapFrom(s => s.Stage!.Name))
            .ForMember(d => d.VillageId, o => o.MapFrom(s => s.Stage!.VillageId))
            .ForMember(d => d.VillageName, o => o.MapFrom(s => s.Stage!.Village!.Name))
            .ForMember(d => d.IsTicketed, o => o.MapFrom(s => s.Production!.IsTicketed))
            .ForMember(d => d.Genres, o => o.MapFrom(s => s.Production!.Genres));
    }
}
