using BookingApp.DomainLayer.Repositories;

namespace BookingApp.Persistance.Repositories;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly HotelDbContext _dbContext;

    public UnitOfWork(HotelDbContext dbContext) => _dbContext = dbContext;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);
}
