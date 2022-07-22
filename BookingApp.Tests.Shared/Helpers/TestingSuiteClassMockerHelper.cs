using BookingApp.DomainLayer.Models;

namespace BookingApp.Tests.Shared.Helpers;

public static class TestingSuiteClassMockerHelper
{
    public static void CreateMocks(
        Fixture fixture,
        out IEnumerable<Hotel> mockHotels, 
        out IEnumerable<HotelDto> mockHotelsDto)
        
    {
        mockHotels = CreateMockHotels(fixture);
        mockHotelsDto = CreateMockHotelDtos(fixture);
        AlterMockHotelIds(mockHotels);
    }

    private static IEnumerable<Hotel> CreateMockHotels(Fixture fixture)
    {
        return fixture.CreateMany<Hotel>(100);
    }

    private static IEnumerable<HotelDto> CreateMockHotelDtos(Fixture fixture)
    {
        return fixture.CreateMany<HotelDto>(1000);
    }

    private static void AlterMockHotelIds(IEnumerable<Hotel> hotels)
    {
        Hotel hotel = hotels.ElementAt(Random.Shared.Next(hotels.Count()));

        hotel.Id = 5;
    }
}
