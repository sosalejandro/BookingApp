using BookingApp.Contracts.DTOs;
using BookingApp.DomainLayer.Exceptions;
using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using BookingApp.DomainLayer.Repositories;
using BookingApp.ServiceLayer.Abstractions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ServiceLayer.Services.V1;

internal sealed partial class HotelService : IHotelService
{
    private readonly IRepositoryManager _repositoryManager;

    public HotelService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager ??
        throw new ArgumentNullException(nameof(_repositoryManager));
    }

    public async Task<HotelDto> CreateAsync(
        HotelForCreationDto hotelForCreatingDto,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = hotelForCreatingDto
            .Adapt<Hotel>();

        _repositoryManager
            .HotelRepository
            .CreateHotel(hotel);

        await _repositoryManager
            .UnitOfWork
            .SaveChangesAsync(stoppingToken);

        return hotel
            .Adapt<HotelDto>();
    }

    public async Task<(IEnumerable<HotelDto> hotels, string ids)> CreateCollectionAsync(IEnumerable<HotelForCreationDto> hotelCollection, CancellationToken stoppingToken = default)
    {
        if (hotelCollection is null)
            throw new HotelCollectionBadRequest();

        var hotelEntities = hotelCollection
            .Adapt<IEnumerable<Hotel>>();

        foreach (Hotel hotel in hotelEntities)
        {
            _repositoryManager.HotelRepository.CreateHotel(hotel);
        }

        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        var hotelCollectionToReturn = hotelEntities
            .Adapt<IEnumerable<HotelDto>>();

        var ids = string.Join(
            ",",
            hotelCollectionToReturn.Select(h => h.Id));

        return (hotels: hotelCollectionToReturn, ids: ids);
    }

    public async Task DeleteAsync(
        int hotelId,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(
            hotelId,
            true,
            stoppingToken);

        _repositoryManager
            .HotelRepository
            .DeleteHotel(hotel);
    }

    public async Task<(IEnumerable<HotelDto> hotelsDto, MetaData metaData)> 
        GetAllAsync(
        HotelParameters hotelParameters,
        CancellationToken stoppingToken = default)
    {
        PagedList<Hotel> hotelsWithMetaData = (PagedList<Hotel>)await _repositoryManager
            .HotelRepository
            .GetAllHotelsAsync(
            hotelParameters,
            false,
            stoppingToken);

        return (hotelsWithMetaData
            .Adapt<IEnumerable<HotelDto>>(),
            hotelsWithMetaData.MetaData);
    }

    public async Task<IEnumerable<HotelDto>> GetByIdsAsync(
        IEnumerable<int> ids,
        HotelParameters hotelParameters,
        CancellationToken stoppingToken = default)
    {

        var hotels = await _repositoryManager
            .HotelRepository.GetByIdsAsync(
            ids,
            hotelParameters,
            false,
            stoppingToken);

        return hotels
            .Adapt<IEnumerable<HotelDto>>();
    }

    public async Task<HotelDto> GetHotelAsync(
        int hotelId,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(
            hotelId,
            false,
            stoppingToken);

        return hotel
            .Adapt<HotelDto>();
    }

    public Task<(HotelForUpdateDto hotelToPatch, Hotel hotelEntity)> 
        GetHotelForPatchAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesForPatchAsync(
        HotelForUpdateDto hotelToPatch, 
        Hotel hotelEntity)
    {
        _ = hotelToPatch.Adapt<Hotel>();
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(
        int hotelId,
        HotelForUpdateDto hotelForUpdateDto,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(
            hotelId,
            true,
            stoppingToken);

        // TODO: implement update logic
        throw new NotImplementedException();

        await _repositoryManager
            .UnitOfWork
            .SaveChangesAsync(stoppingToken);
    }
}
