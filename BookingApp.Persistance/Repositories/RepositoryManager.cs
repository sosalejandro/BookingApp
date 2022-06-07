using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.Repositories;

namespace BookingApp.Persistance.Repositories;
public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IRepository<Hotel>> _lazyHotelRepository;
    private readonly Lazy<IRepository<Room>> _lazyRoomRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

    public RepositoryManager(HotelDbContext dbContext)
    {
        _lazyHotelRepository = new Lazy<IRepository<Hotel>>(() => new HotelRepository(dbContext)) 
            ?? throw new ArgumentNullException(nameof(dbContext));
        _lazyRoomRepository = new Lazy<IRepository<Room>>(() => new RoomRepository(dbContext)) 
            ?? throw new ArgumentNullException(nameof(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext)) 
            ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IRepository<Hotel> HotelRepository => _lazyHotelRepository.Value;

    public IRepository<Room> RoomRepository => _lazyRoomRepository.Value;

    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}
