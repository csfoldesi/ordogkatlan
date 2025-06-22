using API.DTO;
using API.Extensions;
using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        return result.ResultCode switch
        {
            ResultCode.Success => Ok(ApiResponse<T>.Success(result.Value)),
            ResultCode.NotFound => NotFound(),
            ResultCode.Error => BadRequest(ApiResponse<T>.Failure(result.Error)),
            ResultCode.Unauthorized => Unauthorized(result.Error),
            _ => Ok(),
        };
    }

    protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
    {
        return result.ResultCode switch
        {
            ResultCode.Success => Ok(PagedApiResponse<T>.Success(result.Value)),
            ResultCode.NotFound => NotFound(),
            ResultCode.Error => BadRequest(PagedApiResponse<T>.Failure(result.Error)),
            ResultCode.Unauthorized => Unauthorized(result.Error),
            _ => Ok(),
        };

        /*if (result == null)
        {
            return NotFound();
        }
        if (result.ResultCode == ResultCode.Success && result.Value != null)
        {
            Response.AddPaginationHeader(
                result.Value.CurrentPage,
                result.Value.PageSize,
                result.Value.TotalCount,
                result.Value.TotalPages
            );
            return Ok(ApiResponse<PagedList<T>>.Success(result.Value));
        }
        if (result.ResultCode == ResultCode.NotFound)
        {
            return NotFound();
        }
        return BadRequest(result.Error);*/
    }
}
