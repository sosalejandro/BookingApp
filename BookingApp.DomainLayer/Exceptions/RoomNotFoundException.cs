using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Exceptions;
public sealed class RoomNotFoundException : NotFoundException
{
    public RoomNotFoundException(int roomId) 
        : base($"The room with the identifier {roomId} was not found.")
    {
    }
}
