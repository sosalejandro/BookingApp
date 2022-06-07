using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Exceptions;
public sealed class RoomDoesNotBelongToHotelException : BadRequestException
{
    public RoomDoesNotBelongToHotelException(int hotelId, int roomId) 
        : base($"The room with the identifier {roomId} does not belong to the hotel with the identifier {hotelId}")
    {
    }
}
