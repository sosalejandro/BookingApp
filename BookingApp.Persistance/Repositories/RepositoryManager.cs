using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Repositories;

namespace BookingApp.Persistance.Repositories;
public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IHotelRepository> _lazyHotelRepository;
    private readonly Lazy<IRoomRepository> _lazyRoomRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

    public RepositoryManager(HotelDbContext dbContext)
    {
        _lazyHotelRepository = new Lazy<IHotelRepository>(() => new HotelRepository(dbContext)) 
            ?? throw new ArgumentNullException(nameof(dbContext));
        _lazyRoomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(dbContext)) 
            ?? throw new ArgumentNullException(nameof(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext)) 
            ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IHotelRepository HotelRepository => _lazyHotelRepository.Value;

    public IRoomRepository RoomRepository => _lazyRoomRepository.Value;

    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}
