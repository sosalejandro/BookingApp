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
    Task<TEntity?> GetAsync(int id, CancellationToken stoppingToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(PagingParameters paginParameters, CancellationToken stoppingToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, PagingParameters paginParameters, CancellationToken stoppingToken = default);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
}