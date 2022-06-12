using BookingApp.Contracts.Hotel;
using BookingApp.Contracts.Room;

namespace BookingApp.Contracts.DTOs;
public record HotelForCreationDto : HotelForManipulationDto
{
    public IEnumerable<RoomForCreationDto?> Rooms { get; init; }
}
