using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Repositories;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    void CreateAsync(TEntity entity);
    Task<TEntity?> GetAsync(
        int id,
        CancellationToken stoppingToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync<TParameters>(
        TParameters pagingParameters,
        CancellationToken stoppingToken = default) where TParameters : PagingParameters;
    Task<IEnumerable<TEntity>> GetAllAsync<TParameters>(
        Expression<Func<TEntity, bool>> expression,
        TParameters pagingParameters,
        CancellationToken stoppingToken = default) where TParameters : PagingParameters;
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
}