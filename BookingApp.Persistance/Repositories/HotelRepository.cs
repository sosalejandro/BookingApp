using BookingApp.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistance.Repositories;
internal sealed class HotelRepository : EFCoreRepository<Hotel>
{
    public HotelRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
    {
    }
}
