using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Exceptions;

internal sealed class EmptyRoomException : BadRequestException
{
    public EmptyRoomException() : base("Cannot add an empty room.")
    {
    }  
}
