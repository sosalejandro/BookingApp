using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Repositories;

namespace BookingApp.Persistance.Repositories;
internal sealed class RoomRepository : EFCoreRepository<Room>, IRoomRepository
{
    public RoomRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
    {
    }
}
