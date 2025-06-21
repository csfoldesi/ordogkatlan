using Application.Core;
using Application.Core.Interfaces;
using Application.Performance.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Performance;

public class Catalog
{
    public class Query : IRequest<Result<CatalogDto>> { }

    public class Handler : IRequestHandler<Query, Result<CatalogDto>>
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<CatalogDto>> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            var catalog = new CatalogDto();
            catalog.Villages = await _dataContext
                .Villages.Include(v => v.Stages)
                .OrderBy(v => v.Name)
                .ProjectTo<VillageDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            catalog.Stages = await _dataContext
                .Stages.Include(s => s.Village)
                .OrderBy(s => s.Village!.Name)
                .ThenBy(s => s.Name)
                .ProjectTo<StageDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            catalog.Genres = await _dataContext
                .Genres.OrderBy(g => g.Name)
                .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            catalog.Dates = await _dataContext
                .Performances.Select(x => x.StartTime!.Value.Date)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync(cancellationToken);

            return Result<CatalogDto>.Success(catalog);
        }
    }
}
