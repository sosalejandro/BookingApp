using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using BookingApp.DomainLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistance.Repositories;
internal abstract class EFCoreRepository<TClass> : IRepository<TClass> where TClass : BaseEntity
{
    private readonly HotelDbContext _hotelDbContext;

    protected EFCoreRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext
            ?? throw new ArgumentNullException(nameof(hotelDbContext));
    }

    public virtual void CreateAsync(TClass entity)
    {
        if (entity is null)
            throw new ArgumentNullException(
                "Cannot create a new resource from a null object");

        _hotelDbContext.Set<TClass>().Add(entity);
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);

        if (entity is null)
            throw new ArgumentNullException(
                "Cannot delete resource, resource doesn't exist");

        _hotelDbContext.Set<TClass>().Remove(entity);
    }

    public virtual async Task<IEnumerable<TClass>> GetAllAsync<TParameters>(
        TParameters pagingParameters,
        CancellationToken stoppingToken = default) where TParameters : PagingParameters
    {
        return await PagedList<TClass>.ToPagedList(
            _hotelDbContext.Set<TClass>(),
            pagingParameters.PageNumber,
            pagingParameters.PageSize,
            stoppingToken);

        //return await _hotelDbContext
        //    .Set<TClass>()
        //    .Skip((paginParameters.PageNumber - 1) * paginParameters.PageSize)
        //    .Take(paginParameters.PageSize)
        //    .ToListAsync(stoppingToken);
    }

    public virtual async Task<IEnumerable<TClass>> GetAllAsync<TParameters>(
        Expression<Func<TClass, bool>> expression,
        TParameters pagingParameters,
        CancellationToken stoppingToken = default) where TParameters : PagingParameters
    {
        return await PagedList<TClass>.ToPagedList(
            _hotelDbContext.Set<TClass>()
            .Where(expression), 
            pagingParameters.PageNumber, 
            pagingParameters.PageSize, 
            stoppingToken);

        //return await _hotelDbContext
        //   .Set<TClass>()
        //   .Where(expression)
        //   .Skip((paginParameters.PageNumber - 1) * paginParameters.PageSize)
        //   .Take(paginParameters.PageSize)
        //   .ToListAsync(stoppingToken);
    }

    public virtual async Task<TClass?> GetAsync(int id, CancellationToken stoppingToken = default)
    {
        return await _hotelDbContext
          .Set<TClass>()
          .FirstOrDefaultAsync(
            x => x.Id == id,
            stoppingToken);
    }

    public virtual async Task UpdateAsync(TClass entity)
    {
        if (entity is null)
            throw new ArgumentNullException(
                "Cannot update resource a from null object.");

        _hotelDbContext
            .Set<TClass>()
            .Update(entity);

        await Task.CompletedTask;
    }
}
