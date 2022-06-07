using BookingApp.DomainLayer.Models;

namespace BookingApp.Persistance.Repositories;
internal sealed class RoomRepository : EFCoreRepository<Room>
{
    public RoomRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
    {
    }
}
