using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ServiceLayer.Abstractions;
public interface IServiceManager
{
    IHotelService HotelService { get; }
    IRoomService RoomService { get; }
}
