using BookingApp.Contracts.Room;
using BookingApp.DomainLayer.Exceptions;
using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using BookingApp.DomainLayer.Repositories;
using BookingApp.ServiceLayer.Abstractions;
using Mapster;

namespace BookingApp.ServiceLayer.Services.V1;
internal sealed partial class RoomService : IRoomService
{
    private readonly IRepositoryManager _repositoryManager;
    public RoomService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager ??
        throw new ArgumentNullException(nameof(_repositoryManager));
    }

    public async Task<RoomDto> CreateAsync(
        int hotelId,
        RoomForCreationDto roomForCreatingDto,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(
            hotelId,
            true,
            stoppingToken);

        Room room = roomForCreatingDto.Adapt<Room>();

        hotel.AddRoom(room);

        // Maybe redundant
        _repositoryManager.HotelRepository.UpdateHotel(hotel);

        await _repositoryManager.UnitOfWork.SaveChangesAsync(stoppingToken);

        return room.Adapt<RoomDto>();
    }

    public async Task DeleteAsync(
        int hotelId,
        int roomId,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(
            hotelId,
            true,
            stoppingToken);

        Room room = await GetRoomById(
            roomId,
            false,
            stoppingToken);

        CheckForEntityRelationship(hotel, room);

        _repositoryManager.RoomRepository.DeleteRoom(room);

        await _repositoryManager.UnitOfWork.SaveChangesAsync(stoppingToken);
    }

    public Task<IEnumerable<RoomDto>> GetAllAsync(
        RoomParameters roomParameters,
        CancellationToken stoppingToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<RoomDto>> GetAllByHotelAsync(
        int hotelId,
        RoomParameters roomParameters,
        CancellationToken stoppingToken = default)
    {
        var rooms = await _repositoryManager
           .RoomRepository
           .GetRoomsForHotel(
            hotelId,
            roomParameters,
            false,
            stoppingToken);

        var roomsDto = rooms
            .Adapt<IEnumerable<RoomDto>>();

        return roomsDto;
    }

    public async Task<RoomDto> GetByIdAsync(
        int hotelId,
        int roomId,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(
            hotelId,
            false,
            stoppingToken);

        Room room = await GetRoomById(
            roomId,
            false,
            stoppingToken);

        CheckForEntityRelationship(hotel, room);

        RoomDto roomDto = room
            .Adapt<RoomDto>();

        return roomDto;
    }


    public async Task UpdateAsync(
        int hotelId,
        int roomId,
        RoomForUpdateDto roomForUpdateDto,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(
            hotelId,
            true,
            stoppingToken);

        Room room = await GetRoomById(
            roomId,
            true,
            stoppingToken);

        CheckForEntityRelationship(hotel, room);

        // TODO: Room Update logic
        throw new NotImplementedException();

        _repositoryManager.RoomRepository.UpdateRoom(room);

        await _repositoryManager.UnitOfWork.SaveChangesAsync(stoppingToken);
    }
}
