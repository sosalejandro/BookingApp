using BookingApp.DomainLayer.Exceptions;
using BookingApp.DomainLayer.Models;

namespace BookingApp.ServiceLayer.Services.V1;
internal sealed partial class RoomService
{
    private static void CheckForEntityRelationship(Hotel hotel, Room room)
    {
        if (room.HotelId != hotel.Id)
            throw new RoomDoesNotBelongToHotelException(
                hotel.Id, room.Id);
    }

    private async Task<Room> GetRoomById(int roomId, CancellationToken stoppingToken)
    {
        var room = await _repositoryManager
            .RoomRepository
            .GetAsync(roomId, stoppingToken);

        if (room is null)
            throw new RoomNotFoundException(roomId);
        return room;
    }

    private async Task<Hotel> GetHotelById(int hotelId, CancellationToken stoppingToken)
    {
        var hotel = await _repositoryManager
            .HotelRepository
            .GetAsync(hotelId, stoppingToken);

        if (hotel is null)
            throw new HotelNotFoundException(hotelId);
        return hotel;
    }
}
