using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Exceptions;
public sealed class HotelCollectionBadRequest : BadRequestException
{
    public HotelCollectionBadRequest() 
        : base("Hotel collection sent from a client is null")
    {
    }
}
