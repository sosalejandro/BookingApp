using BookingApp.DomainLayer.Exceptions;
using BookingApp.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ServiceLayer.Services.V1;
internal sealed partial class HotelService
{
    private async Task<Hotel> GetHotelById(int hotelId, bool trackChanges, CancellationToken stoppingToken)
    {
        var hotel = await _repositoryManager
            .HotelRepository
            .GetHotelAsync(
            hotelId,
            trackChanges,
            stoppingToken);

        if (hotel is null)
            throw new HotelNotFoundException(hotelId);

        return hotel;
    }
}
