using BookingApp.Contracts.DTOs;
using BookingApp.DomainLayer.Options;
using BookingApp.Presentation.ActionFilters;
using BookingApp.Presentation.ResultFilters;
using BookingApp.ServiceLayer.Abstractions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Presentation.Controllers;

[Route("api/hotels")]
[ApiController]
//[ApiExplorerSettings(GroupName = "v1")]
public class HotelsController : ControllerBase
{
    private readonly IServiceManager _service;

    public HotelsController(IServiceManager service) =>
        _service = service ??
        throw new ArgumentNullException(nameof(service));

    [HttpOptions]
    [ProducesResponseType(200)]
    public IActionResult GetBaseHotelOptions()
    {
        Response.Headers.Add(
            "Allow",
            "GET, OPTIONS, HEAD, POST, PUT, PATCH, DELETE");

        return Ok();
    }

    [HttpHead]
    [HttpGet(Name = "GetHotels")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ServiceFilter(typeof(PaginationHeaderFilterAttribute<HotelDto>))]
    public async Task<IActionResult> GetHotels(
        [FromQuery] HotelParameters hotelParameters, CancellationToken stoppingToken)
    {
        (IEnumerable<HotelDto>, MetaData) result = await _service
            .HotelService
            .GetAllAsync(hotelParameters, stoppingToken);

        if (!result.Item1.Any())
            return NotFound("There are no items in the collection.");

        return Ok(result);
    }

    [HttpGet("{id:int}", Name = "HotelById")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetHotel(int id)
    {
        var hotel = await _service
            .HotelService
            .GetHotelAsync(id);

        return Ok(hotel);
    }

    [HttpPost(Name = "CreateHotel")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateHotel([FromBody] HotelForCreationDto hotel)
    {
        var createdHotel = await _service.HotelService.CreateAsync(hotel);

        return CreatedAtRoute(
            "HotelById",
            new
            {
                id = createdHotel.Id
            },
            createdHotel);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        await _service.HotelService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateHotel
        (int id, [FromBody] HotelForUpdateDto hotel)
    {
        await _service.HotelService.UpdateAsync(id, hotel);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> PartiallyUpdateHotel
        (int id,
        [FromBody] JsonPatchDocument<HotelForUpdateDto> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("patchDoc object sent from client is null.");

        var result = await _service
            .HotelService
            .GetHotelForPatchAsync(id);

        patchDoc.ApplyTo(
            result.hotelToPatch, ModelState);

        TryValidateModel(result.hotelToPatch);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service
            .HotelService
            .SaveChangesForPatchAsync(
            result.hotelToPatch, result.hotelEntity);

        return NoContent();
    }
}
