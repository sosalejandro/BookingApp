using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistance.Repositories;
internal sealed class HotelRepository : EFCoreRepository<Hotel>, IHotelRepository
{
    public HotelRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
    {
    }

    public Task GetHotelsByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }
}
