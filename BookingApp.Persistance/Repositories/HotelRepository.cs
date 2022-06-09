using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Repositories;

namespace BookingApp.Persistance.Repositories;
internal sealed class HotelRepository : EFCoreRepository<Hotel>, IHotelRepository
{
    public HotelRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
    {
    }   
}
