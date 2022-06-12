using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;

namespace BookingApp.DomainLayer.Repositories;
public interface IRoomRepository : IBaseRepository<Room>
{
    void CreateRoom(Room room);
    void DeleteRoom(Room room);
    /// <summary>
    /// Searches for a room within an existing hotel
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <param name="roomId">Room ID</param>
    /// <param name="trackChanges">Tracking changes</param>
    /// <param name="stoppingToken">Cancellation token</param>
    /// <returns>
    /// <c>Room</c>
    /// </returns>
    Task<Room?> GetRoom(
        int roomId,
        bool trackChanges,
        CancellationToken stoppingToken = default);

    /// <summary>
    /// Searches for all rooms within an existing hotel
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <param name="roomParameters">Room pagination and filtering parameters</param>
    /// <param name="trackChanges">Tracking changes</param>
    /// <param name="stoppingToken">Cancellation token</param>
    /// <returns>
    /// Collection of rooms 
    /// </returns>
    Task<IEnumerable<Room>> GetRoomsForHotel(
        int hotelId,
        RoomParameters roomParameters,
        bool trackChanges,
        CancellationToken stoppingToken = default);

    void UpdateRoom(Room room);
}
