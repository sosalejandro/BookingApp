using BookingApp.DomainLayer.Models;

namespace BookingApp.DomainLayer.Repositories;
public interface IRepositoryManager
{
    IHotelRepository HotelRepository { get; }

    IRoomRepository RoomRepository { get; }

    IUnitOfWork UnitOfWork { get; }
}