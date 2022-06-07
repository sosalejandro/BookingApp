using BookingApp.DomainLayer.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace BookingApp.API.Filters;

public class PaginationHeaderActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var results = await next();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
        PagedList<object> resultReq = JsonSerializer.Deserialize<PagedList<object>>(json: results.Result.ToString());
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        context.HttpContext.Response.Headers.Add("X-Pagination", resultReq.MetaData.ToString());
    }
}
