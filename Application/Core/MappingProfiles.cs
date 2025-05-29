using Application.Performance.DTO;
using AutoMapper;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Village, VillageDto>();
        CreateMap<Domain.Stage, StageDto>();
        CreateMap<Domain.Genre, GenreDto>();
    }
}
