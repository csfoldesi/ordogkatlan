using Application.Core;

namespace API.DTO;

public class PagedApiResponse<T> : ApiResponse<T[]>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }

    public static PagedApiResponse<T> Success(PagedList<T>? data) =>
        new()
        {
            IsSuccess = true,
            Data = data != null ? [.. data] : [],
            CurrentPage = data != null ? data.CurrentPage : 0,
            PageSize = data != null ? data.PageSize : 0,
            TotalCount = data != null ? data.TotalCount : 0,
            TotalPages = data != null ? data.TotalPages : 0,
        };
}
