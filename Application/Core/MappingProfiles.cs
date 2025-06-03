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
    }
}
