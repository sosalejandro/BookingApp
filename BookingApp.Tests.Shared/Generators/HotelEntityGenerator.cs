//using AutoFixture.Kernel;
//using BookingApp.DomainLayer.Models;
//using System.Reflection;

//namespace BookingApp.Tests.Shared.Generators;

//internal class HotelEntityGenerator : ISpecimenBuilder
//{
//    public object Create(object request, ISpecimenContext context)
//    {
//        var propertyInfo = request as PropertyInfo;

//        if (propertyInfo is not null && propertyInfo == typeof(Hotel))
//            return CreateHotel();

//        var t = request as Type;
//        if (t is not null && t == typeof(Hotel))
//            return CreateHotel();

//        return new NoSpecimen();
//    }

//    private Hotel CreateHotel()
//    {
//        var fixture = new Fixture();
//        (uint minPrice, uint maxPrice) = GetRandomPrices();
//        (_, uint maxRating) = GetRandomRating();
//        return new Hotel()
//        {
//            Id = fixture.Create<int>(),
//            CreatedDate = fixture.Create<DateTime>(),
//            ModifiedDate = fixture.Create<DateTime>(),
//            Name = fixture.Create<string>(),
//            Type = fixture.Create<string>(),
//            City = GetRandomCity(),
//            Address = ,
//            Description = ,
//            Rating = (int)maxRating,
//            CheapestPrice = minPrice,
//            ExpensiestPrice = maxPrice,
//            Featured = Fixture()
//        };
//    }

//    private string GetRandomCity()
//    {
//        List<string> Cities = new()
//            {
//                "Delaware",
//                "New York",
//                "Caracas",
//                "Los Angeles",
//                "Berlin",
//                "Hong Kong",
//                "London"
//            };

//        return Cities[Random.Shared.Next(Cities.Count)];
//    }

//    private (uint min, uint max) GetRandomPrices()
//    {
//        uint min = (uint)Random.Shared.Next(0, 1000);

//        uint max = (uint)Random.Shared.Next(2, 10);

//        if (DateTime.Now.Ticks % 2 == 0)
//        {
//            max = min * max;
//            return (min, max);
//        }

//        max = min * max / 2;
//        return (min, max);
//    }

//    private (uint min, uint max) GetRandomRating()
//    {
//        uint min = (uint)Random.Shared.Next(0, 5);

//        uint max = (uint)Random.Shared.Next(0, 5);

//        while (!(max >= min))
//        {
//            max = (uint)Random.Shared.Next(0, 5);
//        }

//        return (min, max);
//    }


//}
