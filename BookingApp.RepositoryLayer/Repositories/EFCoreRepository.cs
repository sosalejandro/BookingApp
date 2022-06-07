using BookingApp.DomainLayer.Models;
using BookingApp.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryLayer.Repositories;
public class EFCoreRepository<TClass> : IEFCoreRepository<TClass> where TClass : BaseEntity
{
    private readonly HotelDbContext _hotelDbContext;
    private DbSet<TClass> entities;
    public IUnitOfWork UnitOfWork => _hotelDbContext;

    public EFCoreRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext;
        entities = _hotelDbContext.Set<TClass>();
    }

    public void Create(TClass entity)
    {
        if (entity is null)
            throw new ArgumentNullException(
                "Cannot create a new resource from a null object");

        entities.Add(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await Get(id);

        if (entity is null)
            throw new ArgumentNullException(
                "Cannot delete resource, resource doesn't exist");

        entities.Remove(entity);
    }

    public async Task<TClass?> Get(int id)
    {
        return await entities.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<TClass>> GetAll(int takeCount, int skipCount)
    {
        return await entities
            .Take(takeCount)
            .Skip(skipCount)
            .ToListAsync();
    }

    public async Task<IEnumerable<TClass>> GetAll(Expression<Func<TClass, bool>> expression, int takeCount, int skipCount)
    {
        return await entities
            .Where(expression)
            .Take(takeCount)
            .Skip(skipCount)
            .ToListAsync();
    }

    public async Task Update(TClass entity)
    {
        if (entity is null)
            throw new ArgumentNullException(
                "Cannot update resource a from null object.");

        entities.Update(entity);

        await Task.CompletedTask;
    }
}
