using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using BookingApp.DomainLayer.Repositories;
using BookingApp.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Persistance.Repositories;
internal sealed class RoomRepository 
    : EFCoreBaseRepository<Room>, IRoomRepository
{
    public RoomRepository(HotelDbContext hotelDbContext) 
        : base(hotelDbContext)
    {
    }

    public void CreateRoom(Room room)
    {
        Create(room);
    }

    public void DeleteRoom(Room room)
    {
        Delete(room);
    }

    public async Task<Room?> GetRoom(
        int roomId,
        bool trackChanges,
        CancellationToken stoppingToken = default)
    {
        return await FindByCondition(
            r => r.Id.Equals(roomId),
            trackChanges)
            .SingleOrDefaultAsync(stoppingToken);
    }

    public async Task<IEnumerable<Room>> GetRoomsForHotel(
        int hotelId,
        RoomParameters roomParameters,
        bool trackChanges,
        CancellationToken stoppingToken)
    {
        var rooms = await FindByCondition(
            r => r.HotelId.Equals(hotelId),
            trackChanges)
            .FilterRooms(roomParameters)
            .Search(roomParameters.SearchTerm)
            .Sort(roomParameters.OrderBy)
            .ToListAsync(stoppingToken);


        return PagedList<Room>.ToPagedList(rooms,
            roomParameters.PageNumber,
            roomParameters.PageSize);
    }

    public void UpdateRoom(Room room)
    {
        Update(room);
    }
}