using AutoFixture.Kernel;
using System.Reflection;

namespace BookingApp.Fixtures.Shared.Generators;

public class HotelParametersGenerator : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var propertyInfo = request as PropertyInfo;

        if (propertyInfo is not null && propertyInfo == typeof(HotelParameters))
            return CreateHotelParams();

        var t = request as Type;
        if (t is not null && t == typeof(HotelParameters))
            return CreateHotelParams();

        return new NoSpecimen();
    }

    private HotelParameters CreateHotelParams()
    {
        (uint minPrice, uint maxPrice) = GetRandomPrices();
        (uint minRating, uint maxRating) = GetRandomRating();
        return new HotelParameters()
        {
            City = GetRandomCity(),
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            MinRating = minRating,
            MaxRating = maxRating,
            PageNumber = Random.Shared.Next(0, 20),
            PageSize = Random.Shared.Next(10, 50),
            SearchTerm = GetRandomSearchTerm(),
            OrderBy = GetRandomOrder()
        };
    }

    private string GetRandomSearchTerm()
    {
        List<string?> SearchTerms = new()
            {
                "Mic",
                null,
                "Hilton",
                null,
                "Buil",
                "Hotel",
                null
            };

        return SearchTerms[Random.Shared.Next(SearchTerms.Count)];
    }

    private string GetRandomCity()
    {
        List<string> Cities = new()
            {
                "Delaware",
                "New York",
                "Caracas",
                "Los Angeles",
                "Berlin",
                "Hong Kong",
                "London"
            };

        return Cities[Random.Shared.Next(Cities.Count)];
    }

    private (uint min, uint max) GetRandomPrices()
    {
        uint min = (uint)Random.Shared.Next(0, 1000);

        uint max = (uint)Random.Shared.Next(2, 10);

        if (DateTime.Now.Ticks % 2 == 0)
        {
            max = min * max;
            return (min, max);
        }

        max = min * max / 2;
        return (min, max);
    }

    private (uint min, uint max) GetRandomRating()
    {
        uint min = (uint)Random.Shared.Next(0, 5);

        uint max = (uint)Random.Shared.Next(0, 5);

        while (!(max >= min))
        {
            max = (uint)Random.Shared.Next(0, 5);
        }

        return (min, max);
    }

    private string GetRandomOrder()
    {
        List<string> OrderByList = new()
            {
                "name",
                "name desc,",
                "name,rating desc",
                "rating",
                "price desc",
                "price",
                "name,rating desc, price"
            };

        return OrderByList[Random.Shared.Next(OrderByList.Count)];
    }
}
