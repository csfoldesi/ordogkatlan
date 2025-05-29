using Application.Performance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CatalogController : BaseApiController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var result = await Mediator.Send(new Catalog.Query { });
        return HandleResult(result);
    }
}
