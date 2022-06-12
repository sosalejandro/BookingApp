using BookingApp.Contracts.DTOs;
using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;


namespace BookingApp.ServiceLayer.Abstractions;

public interface IHotelService
{
    Task<(IEnumerable<HotelDto> hotelsDto, MetaData metaData)> GetAllAsync(
        HotelParameters hotelParameters,
        CancellationToken stoppingToken = default);
    Task<IEnumerable<HotelDto>> GetByIdsAsync(
        IEnumerable<int> ids,
        HotelParameters hotelParameters,
        CancellationToken stoppingToken = default);
    Task<(HotelForUpdateDto hotelToPatch, Hotel hotelEntity)> 
        GetHotelForPatchAsync(int id);
    Task SaveChangesForPatchAsync(
        HotelForUpdateDto hotelToPatch,
        Hotel hotelEntity);
    Task<HotelDto> GetHotelAsync(
        int hotelId,
        CancellationToken stoppingToken = default);
    Task<HotelDto> CreateAsync(
        HotelForCreationDto hotelForCreatingDto,
        CancellationToken stoppingToken = default);
    Task<(IEnumerable<HotelDto> hotels, string ids)> CreateCollectionAsync(
        IEnumerable<HotelForCreationDto> hotelCollection,
        CancellationToken stoppingToken = default);
    Task UpdateAsync(
        int hotelId,
        HotelForUpdateDto hotelForUpdateDto,
        CancellationToken stoppingToken = default);
    Task DeleteAsync(
        int hotelId,
        CancellationToken stoppingToken = default);
}
