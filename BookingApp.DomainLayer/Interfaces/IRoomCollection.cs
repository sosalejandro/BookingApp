using BookingApp.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Interfaces;
public interface IRoomCollection
{
    public ICollection<Room>? Rooms { get; set; }
}
