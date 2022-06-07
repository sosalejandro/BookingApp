using BookingApp.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.RepositoryLayer.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    void Create(TEntity entity);
    Task<TEntity?> Get(int id);
    Task<IEnumerable<TEntity>> GetAll(int takeCount, int skipCount);
    Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression, int takeCount, int skipCount);
    Task Update(TEntity entity);
    Task Delete(int id);
}
