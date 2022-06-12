using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;

namespace BookingApp.DomainLayer.Repositories;
public interface IHotelRepository : IBaseRepository<Hotel>
{
    void CreateHotel(Hotel hotel);
    void DeleteHotel(Hotel hotel);
    Task<IEnumerable<Hotel>> GetAllHotelsAsync(
        HotelParameters hotelParameters,
        bool trackChanges,
        CancellationToken stoppingToken = default);
    Task<IEnumerable<Hotel>> GetByIdsAsync(
        IEnumerable<int> ids,
        HotelParameters hotelParameters,
        bool trackChanges,
        CancellationToken stoppingToken = default);
    Task<Hotel?> GetHotelAsync(
        int id,
        bool trackChanges,
        CancellationToken stoppingToken = default);
    void UpdateHotel(Hotel hotel);
}
