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
        MetaData = new(count, pageSize, pageNumber, (int)Math.Ceiling(count / (double)pageSize));

        AddRange(items);
    }

    public async static Task<PagedList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken stoppingToken = default)
    {
        var count = source.Count();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(stoppingToken);

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
