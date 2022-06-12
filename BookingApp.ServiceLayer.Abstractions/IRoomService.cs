using BookingApp.Contracts.Room;
using BookingApp.DomainLayer.Options;

namespace BookingApp.ServiceLayer.Abstractions;
public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllAsync(RoomParameters roomParameters, CancellationToken stoppingToken = default);
    Task<IEnumerable<RoomDto>> GetAllByHotelAsync(int hotelId, RoomParameters roomParameters, CancellationToken stoppingToken = default);
    Task<RoomDto> GetByIdAsync(int hotelId, int roomId, CancellationToken stoppingToken = default);
    Task<RoomDto> CreateAsync(int hotelId, RoomForCreationDto roomForCreatingDto, CancellationToken stoppingToken = default);
    Task UpdateAsync(int hotelId, int roomId, RoomForUpdateDto roomForUpdateDto, CancellationToken stoppingToken = default);
    Task DeleteAsync(int hotelId, int roomId, CancellationToken stoppingToken = default);
}
