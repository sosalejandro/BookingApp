using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using System.Reflection;
using System.Linq.Dynamic.Core;
using System.Text;

namespace BookingApp.Persistance.Extensions;
internal static class RoomRepositoryExtensions
{
    public static IQueryable<Room> FilterRooms(
        this IQueryable<Room> rooms,
        RoomParameters roomParameters)
    {
        return rooms.Where(r => 
        (r.Price >= roomParameters.MinPrice &&
        r.Price <= roomParameters.MaxPrice)
        && 
        (r.Capacity >= roomParameters.MinCapacity &&
        r.Capacity <= roomParameters.MaxCapacity));
    }

    public static IQueryable<Room> Search(
        this IQueryable<Room> rooms,
        string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return rooms;

        string lowerCaseTerm = searchTerm.Trim().ToLower();

        return rooms
            .Where(
            r => r.Name.ToLower()
            .Contains(lowerCaseTerm)
            );
    }

    public static IQueryable<Room> Sort(
        this IQueryable<Room> rooms,
        string OrderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(OrderByQueryString))
            return rooms.OrderBy(e => e.Name);

        string[] orderParams = OrderByQueryString.
            Trim().Split(',');
        PropertyInfo[] propertyInfos = typeof(Room)
            .GetProperties(
            BindingFlags.Public | BindingFlags.Instance);

        StringBuilder orderQueryBuilder = new();

        #region Reflection Sorting StringBuilder Algo
        foreach (string param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param
                .Split(" ")[0];
            var objectProperty = propertyInfos
                .FirstOrDefault(pi =>
            pi.Name.Equals(
                propertyFromQueryName, 
                StringComparison.CurrentCultureIgnoreCase)
            );

            if (objectProperty is null)
                continue;

            var direction = param.EndsWith(" desc") 
                ? "descending" : "ascending";

            orderQueryBuilder
                .Append(
                $"{objectProperty.Name.ToString()} {direction}");
        }
        #endregion

        var orderQuery = orderQueryBuilder
            .ToString()
            .TrimEnd(',', ' ');

        if (string.IsNullOrWhiteSpace(orderQuery))
            return rooms.OrderBy(e => e.Name);

        return rooms.OrderBy(orderQuery);
    }
}
