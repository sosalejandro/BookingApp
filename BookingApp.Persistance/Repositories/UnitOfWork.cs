using BookingApp.DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistance.Repositories;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly HotelDbContext _dbContext;

    public UnitOfWork(HotelDbContext dbContext) => _dbContext = dbContext;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
