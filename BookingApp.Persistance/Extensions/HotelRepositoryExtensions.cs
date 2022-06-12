using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using System.Reflection;
using System.Linq.Dynamic.Core;
using System.Text;

namespace BookingApp.Persistance.Extensions;
internal static class HotelRepositoryExtensions
{
    public static IQueryable<Hotel> FilterHotels(
        this IQueryable<Hotel> hotels,
        HotelParameters hotelParameters)
    {
        return hotels.Where(h => 
        (h.Rating >= hotelParameters.MinRating &&
        h.Rating <= hotelParameters.MaxRating) 
        && 
        (h.CheapestPrice >= hotelParameters.MinPrice &&
        h.CheapestPrice <= hotelParameters.MaxPrice));
    }

    public static IQueryable<Hotel> Search(
       this IQueryable<Hotel> rooms,
       string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return rooms;

        string lowerCaseTerm = searchTerm.Trim().ToLower();

        return rooms.Where(h => h.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Hotel> Sort(this IQueryable<Hotel> hotels, string OrderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(OrderByQueryString))
            return hotels.OrderBy(e => e.Name);

        string[] orderParams = OrderByQueryString.Trim().Split(',');
        PropertyInfo[] propertyInfos = typeof(Room)
            .GetProperties(
            BindingFlags.Public | BindingFlags.Instance);

        StringBuilder orderQueryBuilder = new();

        foreach (string param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi =>
            pi.Name.Equals(propertyFromQueryName, StringComparison.CurrentCultureIgnoreCase));

            if (objectProperty is null)
                continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

        if (string.IsNullOrWhiteSpace(orderQuery))
            return hotels.OrderBy(e => e.Name);

        return hotels.OrderBy(orderQuery);
    }
}
