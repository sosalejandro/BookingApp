using BookingApp.Contracts.DTOs;
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
        Hotel hotel = hotelForCreatingDto.Adapt<Hotel>();

        _repositoryManager.HotelRepository.CreateAsync(hotel);

        await _repositoryManager.UnitOfWork.SaveChangesAsync(stoppingToken);

        return hotel.Adapt<HotelDto>();
    }

    public async Task DeleteAsync(
        int hotelId,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(hotelId, stoppingToken);

        await _repositoryManager.HotelRepository.DeleteAsync(hotel.Id);
    }

    public async Task<IEnumerable<HotelDto>> GetAllAsync(
        PagingParameters paginParameters,
        CancellationToken stoppingToken = default)
    {
        IEnumerable<Hotel> hotels = await _repositoryManager
            .HotelRepository
            .GetAllAsync(paginParameters, stoppingToken);

        return hotels.Adapt<IEnumerable<HotelDto>>();
    }

    public async Task<HotelDto> GetByIdAsync(
        int hotelId,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(hotelId, stoppingToken);

        return hotel.Adapt<HotelDto>();
    }

    public async Task UpdateAsync(
        int hotelId,
        HotelForUpdateDto hotelForUpdateDto,
        CancellationToken stoppingToken = default)
    {
        Hotel hotel = await GetHotelById(hotelId, stoppingToken);

        // TODO: implement update logic
        throw new NotImplementedException();

        await _repositoryManager.UnitOfWork.SaveChangesAsync(stoppingToken);
    }
}
