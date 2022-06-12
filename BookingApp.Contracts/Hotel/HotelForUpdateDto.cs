using BookingApp.Contracts.Hotel;
using BookingApp.Contracts.Room;
using BookingApp.DomainLayer.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Contracts.DTOs;

public record HotelForUpdateDto : HotelForManipulationDto
{
    public IEnumerable<RoomForCreationDto?> Rooms { get; init; }
}
