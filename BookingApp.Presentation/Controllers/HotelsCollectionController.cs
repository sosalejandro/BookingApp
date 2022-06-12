using BookingApp.Contracts.DTOs;
using BookingApp.DomainLayer.Options;
using BookingApp.Presentation.ActionFilters;
using BookingApp.Presentation.ModelBinders;
using BookingApp.Presentation.ResultFilters;
using BookingApp.ServiceLayer.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Presentation.Controllers;
[Route("api/hotels/collection")]
[ApiController]
//[ApiExplorerSettings(GroupName = "v1")]
public class HotelsCollectionController : ControllerBase
{
    private readonly IServiceManager _service;

    public HotelsCollectionController(IServiceManager service) => 
        _service = service ?? 
        throw new ArgumentNullException(nameof(service));

    [HttpOptions]
    [ProducesResponseType(200)]
    public IActionResult GetBaseHotelCollectionoptions()
    {
        Response.Headers.Add(
           "Allow",
           "GET, OPTIONS, HEAD, POST");

        return Ok();
    }

    [HttpHead]
    [HttpGet("({ids})", Name = "HotelCollection")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ServiceFilter(typeof(PaginationHeaderFilterAttribute<HotelDto>))]
    public async Task<IActionResult> GetHotelCollection
       ([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids,
       [FromQuery] HotelParameters hotelParameters)
    {
        var hotels = await _service
        .HotelService
        .GetByIdsAsync(ids, hotelParameters);

        return Ok(hotels);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateHotelCollection
    ([FromBody] IEnumerable<HotelForCreationDto> hotelCollection)
    {
        var result = await _service
            .HotelService
            .CreateCollectionAsync(
            hotelCollection);

        return CreatedAtRoute("HotelCollection", new { result.ids }, result.hotels);
    }
}
