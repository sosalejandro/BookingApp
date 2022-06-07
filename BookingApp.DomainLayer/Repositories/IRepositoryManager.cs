using BookingApp.DomainLayer.Models;

namespace BookingApp.DomainLayer.Repositories;
public interface IRepositoryManager
{
    IRepository<Hotel> HotelRepository { get; }

    IRepository<Room> RoomRepository { get; }

    IUnitOfWork UnitOfWork { get; }
}