using Application.Core;
using Application.Core.Interfaces;
using Application.Performance.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Performance;

public class List
{
    public class Query : IRequest<Result<PagedList<PerformanceDto>>>
    {
        public DateTime[]? Dates { get; set; } = [];
        public string[]? Villages { get; set; } = [];
        public string[]? Stages { get; set; } = [];
        public string[]? Genres { get; set; } = [];
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<PerformanceDto>>>
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<PerformanceDto>>> Handle(
            Query request,
            CancellationToken cancellationToken
        )
        {
            var query = _dataContext
                .Performances.Include(p => p.Production)
                .ThenInclude(pr => pr.Genres)
                .ProjectTo<PerformanceDto>(_mapper.ConfigurationProvider);

            if (request.Dates?.Length > 0)
            {
                query = query.Where(p => request.Dates!.Contains(p.Date));
            }

            if (request.Villages?.Length > 0)
            {
                query = query.Where(p => request.Villages!.Contains(p.VillageId));
            }

            if (request.Stages?.Length > 0)
            {
                query = query.Where(p => request.Stages!.Contains(p.StageId));
            }

            // TODO: add Genres filtering


            query = query.OrderBy(p => p.Date).ThenBy(p => p.Title);

            var result = await PagedList<PerformanceDto>.CreateAsync(
                query,
                request.PageNumber,
                request.PageSize
            );

            return Result<PagedList<PerformanceDto>>.Success(result);
        }
    }
}
