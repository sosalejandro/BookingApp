using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Exceptions;
public sealed class HotelNotFoundException : NotFoundException
{
    public HotelNotFoundException(int hotelId) 
        : base($"The hotel with the identifier {hotelId} was not found.")
    {
    }
}
