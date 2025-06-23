using API.DTO;
using Application.Performance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PerformancesController : BaseApiController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> DownloadProgram()
    {
        var result = await Mediator.Send(
            new Download.Command { BaseUrl = "https://idevelopment.hu/2025.json" }
        );
        return HandleResult(result);
    }

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> List([FromQuery] PerformanceSearchRequest request)
    {
        var result = await Mediator.Send(
            new List.Query
            {
                Dates =
                    request.Dates != null ? [.. request.Dates.Select(d => DateTime.Parse(d))] : [],
                Villages = request.Villages,
                Stages = request.Stages,
                Genres = request.Genres,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            }
        );
        return HandlePagedResult(result);
    }
}
