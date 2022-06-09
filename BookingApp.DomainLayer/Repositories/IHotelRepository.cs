using BookingApp.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Repositories;
public interface IHotelRepository : IRepository<Hotel>
{
    Task GetHotelsByCategory(int categoryId);
}
