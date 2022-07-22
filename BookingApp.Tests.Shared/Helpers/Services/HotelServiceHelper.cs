using BookingApp.DomainLayer.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Fixtures.Shared.Helpers.Services;

internal static class HotelServiceHelper
{
    public static void MockGetAllAsync(
        Mock<IServiceManager> mockServiceManager,
        IEnumerable<HotelDto> mockHotels,
        HotelParameters hotelParams)
    {
        GetPaginationReturnValues(
            mockHotels,
            hotelParams,
            out List<HotelDto> items,
            out MetaData metaData);

        mockServiceManager
            .Setup(sm => sm
            .HotelService
            .GetAllAsync(
                It.IsAny<HotelParameters>(),
                It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync((items, metaData));
    }

    public static void MockGetByIdsAsync()
    {

    }

    public static void MockSaveChangesForPatchAsync()
    {

    }

    public static void MockGetHotelAsync(
         Mock<IServiceManager> mockServiceManager)
    {
        mockServiceManager
            .Setup(sm => sm
            .HotelService
            .GetHotelAsync(
                5,
                It.IsAny<CancellationToken>()
                ));
    }

    public static void MockGetHotelAsync(
         Mock<IServiceManager> mockServiceManager,
        IEnumerable<Hotel> mockHotels)
    {
        HotelDto result = new() { Id = 5 };

        mockServiceManager
            .Setup(sm => sm
            .HotelService
            .GetHotelAsync(
                5,
                It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync(result);
    }

    public static void MockCreateAsync(
        Mock<IServiceManager> mockServiceManager,
        IEnumerable<Hotel> mockHotels)
    {
        HotelDto result = new() { Id = 5 };

        mockServiceManager
          .Setup(sm => sm
          .HotelService
          .CreateAsync(
              It.IsAny<HotelForCreationDto>(),
              It.IsAny<CancellationToken>()
              ))
          .ReturnsAsync(result);
    }

    public static void MockCreateCollectionAsync(Mock<IServiceManager> mockServiceManager)
    {
        //mockServiceManager
        //    .Setup(sm => sm
        //    .HotelService
        //    .UpdateAsync(
        //        It.IsAny<int>(),
        //        It.IsAny<HotelForUpdateDto>(),
        //        It.IsAny<CancellationToken>()
        //        ));
    }

    public static void MockUpdateAsync(Mock<IServiceManager> mockServiceManager)
    {
        mockServiceManager
            .Setup(sm => sm
            .HotelService
            .UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<HotelForUpdateDto>(),
                It.IsAny<CancellationToken>()
                ));
    }

    public static void MockDeleteAsync(Mock<IServiceManager> mockServiceManager)
    {
        mockServiceManager
            .Setup(sm => sm
            .HotelService
            .DeleteAsync(
                It.IsAny<int>(), 
                It.IsAny<CancellationToken>()
                ));
    }

    private static void GetPaginationReturnValues(
        IEnumerable<HotelDto> mockHotels,
        HotelParameters hotelParams,
        out List<HotelDto> items,
        out MetaData metaData)
    {
        int count = mockHotels.Count();
        items = mockHotels
            .Skip((hotelParams.PageNumber - 1) * hotelParams.PageSize)
            .Take(hotelParams.PageSize)
            .ToList();

        metaData = new(
            hotelParams.PageNumber,
            (int)Math.Ceiling(count / (double)hotelParams.PageSize),
            hotelParams.PageSize,
            count);
    }
}
