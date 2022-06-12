using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using BookingApp.DomainLayer.Repositories;
using BookingApp.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Persistance.Repositories;
internal sealed class HotelRepository 
    : EFCoreBaseRepository<Hotel>, IHotelRepository
{

    public HotelRepository(HotelDbContext hotelDbContext) 
        : base(hotelDbContext)
    {
    }

    public void CreateHotel(Hotel hotel)
    {
        Create(hotel);
    }

    public void DeleteHotel(Hotel hotel)
    {
        Delete(hotel) ;
    }

    public async Task<IEnumerable<Hotel>> GetAllHotelsAsync(
        HotelParameters hotelParameters,
        bool trackChanges,
        CancellationToken stoppingToken = default)
    {
        var hotels = await FindAll(trackChanges)
            .FilterHotels(hotelParameters)
            .Search(hotelParameters.SearchTerm)
            .Sort(hotelParameters.OrderBy)
            .ToListAsync(stoppingToken);

        return PagedList<Hotel>.ToPagedList(
            hotels,
            hotelParameters.PageNumber,
            hotelParameters.PageSize);
    }

    public async Task<IEnumerable<Hotel>> GetByIdsAsync(
        IEnumerable<int> ids,
        HotelParameters hotelParameters,
        bool trackChanges,
        CancellationToken stoppingToken = default)
    {
        var hotels = await FindByCondition(
            h => ids.Contains(h.Id),
            trackChanges)
            .FilterHotels(hotelParameters)
            .Search(hotelParameters.SearchTerm)
            .Sort(hotelParameters.OrderBy)
            .ToListAsync(stoppingToken);

        return PagedList<Hotel>.ToPagedList(
            hotels,
            hotelParameters.PageNumber,
            hotelParameters.PageSize);
    }

    public async Task<Hotel?> GetHotelAsync(
        int id,
        bool trackChanges,
        CancellationToken stoppingToken = default)
    {
        return await FindByCondition(
            h => h.Id.Equals(id),
            trackChanges)
            .SingleOrDefaultAsync(stoppingToken);
    }

    public void UpdateHotel(Hotel hotel)
    {
        Update(hotel);
    }
}
