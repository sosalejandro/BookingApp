
using BookingApp.DomainLayer.Models;
using BookingApp.Fixtures.Shared.Helpers.Services;
using BookingApp.Tests.Shared;

namespace BookingApp.Fixtures.Shared.Helpers;

public static class ServiceManagerHelper
{
    public static void RegisterMockHotelService(
        Mock<IServiceManager> mockServiceManager,
        IEnumerable<Hotel> mockHotels,
        TestingSuite testingSuite)
    {

        if (testingSuite is TestingSuite.GetHotelAsync)
        {
            HotelServiceHelper.MockGetHotelAsync(mockServiceManager, mockHotels);
        }

        if (testingSuite is TestingSuite.CreateAsync)
        {
            HotelServiceHelper.MockCreateAsync(mockServiceManager, mockHotels);
        }
    }

    public static void RegisterMockHotelService(
        Mock<IServiceManager> mockServiceManager,
        TestingSuite testingSuite)
    {

        if (testingSuite is TestingSuite.DeleteAsync)
        {
            HotelServiceHelper.MockDeleteAsync(mockServiceManager);
        }

        if (testingSuite is TestingSuite.GetAllEmptyAsync)
        {
            HotelServiceHelper.MockGetHotelAsync(mockServiceManager);
        }

        if (testingSuite is TestingSuite.UpdateAsync)
        {
            HotelServiceHelper.MockUpdateAsync(mockServiceManager);
        }
    }

    public static void RegisterMockHotelService(
      Mock<IServiceManager> mockServiceManager,
      IEnumerable<HotelDto> mockHotelDtos,
      TestingSuite testingSuite,
      HotelParameters hotelParams)
    {
        if (testingSuite is TestingSuite.GetAllAsync)
        {
            //IEnumerable<HotelDto> mockHotelDtos = CreateMockHotelDtos(fixture);
            HotelServiceHelper.MockGetAllAsync(mockServiceManager, mockHotelDtos, hotelParams);
        }

    }   
}
