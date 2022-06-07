using BookingApp.Contracts.DTOs;
using BookingApp.DomainLayer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ServiceLayer.Abstractions;

public interface IHotelService
{
    Task<IEnumerable<HotelDto>> GetAllAsync(PagingParameters paginParameters, CancellationToken stoppingToken = default);
    Task<HotelDto> GetByIdAsync(int hotelId, CancellationToken stoppingToken = default);
    Task<HotelDto> CreateAsync(HotelForCreationDto hotelForCreatingDto, CancellationToken stoppingToken = default);
    Task UpdateAsync(int hotelId, HotelForUpdateDto hotelForUpdateDto, CancellationToken stoppingToken = default);
    Task DeleteAsync(int hotelId, CancellationToken stoppingToken = default);
}
