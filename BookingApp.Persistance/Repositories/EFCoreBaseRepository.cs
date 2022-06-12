using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistance.Repositories;
internal abstract class EFCoreBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly HotelDbContext _hotelDbContext;

    protected EFCoreBaseRepository(HotelDbContext hotelDbContext)
    {
        _hotelDbContext = hotelDbContext 
            ?? throw new ArgumentNullException(nameof(hotelDbContext));
    }

    public void Create(TEntity entity)
    {
        _hotelDbContext.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _hotelDbContext.Set<TEntity>().Remove(entity);
    }

    public IQueryable<TEntity> FindAll(bool trackChanges)
    {
        return !trackChanges ? 
            _hotelDbContext.Set<TEntity>().AsNoTracking() :
            _hotelDbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges)
    {
        return !trackChanges ?
            _hotelDbContext.Set<TEntity>()
            .Where(expression)
            .AsNoTracking() :
            _hotelDbContext.Set<TEntity>()
            .Where(expression);
    }

    public void Update(TEntity entity)
    {
        _hotelDbContext.Set<TEntity>().Update(entity);
    }
}
