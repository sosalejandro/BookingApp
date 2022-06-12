using BookingApp.Contracts.DTOs;
using BookingApp.DomainLayer.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace BookingApp.Presentation.ActionFilters;
public class PaginationHeaderFilterAttribute<TResult> : IAsyncResultFilter
{   
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var results = context.Result;        

#pragma warning disable CS8605 // Unboxing a possibly null value.
        if (context.HttpContext.Response.StatusCode is 200)
        {
            OkObjectResult resultObjectArray = (OkObjectResult)results;
            (IEnumerable<TResult>, MetaData) tupleResult = ((IEnumerable<TResult>, MetaData))resultObjectArray.Value;
            context.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(tupleResult.Item2));

            resultObjectArray.Value = tupleResult.Item1;
        }
        await next();

#pragma warning restore CS8605 // Unboxing a possibly null value.



    }
}
