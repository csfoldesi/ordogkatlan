using System.Globalization;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Web;
using Application.Core;
using Application.Core.Interfaces;
using Application.Performance.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Performance;

public class Download
{
    public class Command : IRequest<Result<string>>
    {
        public required string BaseUrl { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly HttpClient _httpClient;
        private readonly IDataContext _dataContext;

        public Handler(IDataContext dataContext)
        {
            _httpClient = new HttpClient();
            _dataContext = dataContext;
        }

        public async Task<Result<string>> Handle(
            Command request,
            CancellationToken cancellationToken
        )
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                request.BaseUrl,
                cancellationToken
            );
            if (response.IsSuccessStatusCode)
            {
                var jsonInput = await response.Content.ReadFromJsonAsync<JsonInputRoot>(
                    cancellationToken
                );

                var performances = ParsePerformances(jsonInput!);

                await using var transaction = await _dataContext.BeginTransactionAsync(
                    cancellationToken
                );
                foreach (var p in performances)
                {
                    var village = await _dataContext.Villages.FindAsync(
                        [p.Stage!.VillageId],
                        cancellationToken: cancellationToken
                    );
                    if (village == null)
                    {
                        village = new Domain.Village
                        {
                            Id = p.Stage.VillageId,
                            Name = p.Stage.Village!.Name,
                        };
                        _dataContext.Villages.Add(village);
                    }

                    var stage = await _dataContext.Stages.FindAsync(
                        [p.Stage.Id],
                        cancellationToken: cancellationToken
                    );
                    if (stage == null)
                    {
                        stage = new Domain.Stage
                        {
                            Id = p.Stage.Id,
                            Name = p.Stage.Name,
                            VillageId = village.Id,
                        };
                        _dataContext.Stages.Add(stage);
                    }

                    var production = await _dataContext
                        .Productions.Include(pr => pr.Genres)
                        .FirstOrDefaultAsync(pr => pr.Id == p.Production!.Id, cancellationToken);
                    if (production != null)
                    {
                        production.Genres.Clear();
                    }
                    else
                    {
                        production = new Domain.Production
                        {
                            Id = p.Production!.Id,
                            Title = p.Production!.Title,
                        };
                        _dataContext.Productions.Add(production);
                    }
                    production.Title = p.Production!.Title;
                    production.Description = p.Production!.Description;
                    production.Thumbnail = p.Production!.Thumbnail;
                    production.IsTicketed = p.Production!.IsTicketed;

                    foreach (var g in p.Production.Genres)
                    {
                        var genre = await _dataContext.Genres.FindAsync(
                            [g.Id],
                            cancellationToken: cancellationToken
                        );
                        if (genre == null)
                        {
                            genre = new Domain.Genre { Id = g.Id, Name = g.Name };
                            _dataContext.Genres.Add(genre);
                        }
                        production.Genres.Add(genre);
                    }

                    var performance = await _dataContext.Performances.FirstOrDefaultAsync(
                        x => x.ProductionId == p.ProductionId && x.StartTime == p.StartTime,
                        cancellationToken: cancellationToken
                    );
                    if (performance == null)
                    {
                        performance = new Domain.Performance
                        {
                            StageId = p.StageId,
                            ProductionId = production.Id,
                            Production = production,
                        };
                        _dataContext.Performances.Add(performance);
                    }
                    performance.StartTime = p.StartTime;
                    performance.EndTime = p.EndTime;
                    performance.Duration = p.Duration;
                    performance.StageId = stage.Id;
                    performance.Stage = stage;
                }
                await _dataContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }

            return Result<string>.Success("Ok");
        }

        private List<Domain.Performance> ParsePerformances(JsonInputRoot jsonInput)
        {
            List<Domain.Performance> result = [];
            foreach (var item in jsonInput.Data.Centerpiece)
            {
                var productionId = item.ProductionId.Split("/")[^1];
                var description = item.Description;
                var title = item.Title;
                var genres = item
                    .Labels.Select(label => new Domain.Genre
                    {
                        Id = label.Href!.Split("/")[^1],
                        Name = label.Name!,
                    })
                    .ToList();
                DateTime? startTime = null;
                DateTime? endTime = null;
                int? duration = null;
                if (item.Time != null)
                {
                    startTime = DateTime.ParseExact(
                        item.Time.Name!,
                        "MMM d. (dddd) HH:mm",
                        new CultureInfo("hu-HU")
                    );
                    duration = item.Duration;
                    endTime = startTime?.AddMinutes(duration != null ? (double)duration! : 0);
                }
                var village = new Domain.Village
                {
                    Id = item.Village.Href!.Substring(1),
                    Name = item.Village.Name!,
                };
                var stage = new Domain.Stage
                {
                    Id = item.Stage.Href!.Substring(1),
                    Name = item.Stage.Name!,
                    VillageId = village.Id,
                    Village = village,
                };
                var thumbnail = item.Thumbnail?.Mobile;

                var isTicketed = item.Ticketed != null;

                var production = new Domain.Production
                {
                    Id = productionId,
                    Title = title,
                    Description = StripHtml(description ?? ""),
                    Thumbnail = thumbnail,
                    IsTicketed = isTicketed,
                    Genres = genres,
                };

                var performance = new Domain.Performance
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration,
                    StageId = stage.Id,
                    Stage = stage,
                    ProductionId = production.Id,
                    Production = production,
                };

                result.Add(performance);
            }
            return result;
        }

        private string StripHtml(string text)
        {
            string result = Regex.Replace(text, "<.*?>", string.Empty);
            result = HttpUtility.HtmlDecode(result);
            return result.Trim();
        }
    }
}
