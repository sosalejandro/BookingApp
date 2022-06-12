using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Options;
public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; private set; }
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new(
            pageNumber,
            (int)Math.Ceiling(count / (double)pageSize),
            pageSize,
            count
            );

        AddRange(items);
    }

    public static PagedList<T> ToPagedList(
        IEnumerable<T> source,
        int pageNumber,
        int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
