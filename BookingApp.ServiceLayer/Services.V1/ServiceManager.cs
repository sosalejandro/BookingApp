using BookingApp.DomainLayer.Repositories;
using BookingApp.ServiceLayer.Abstractions;

namespace BookingApp.ServiceLayer.Services.V1;
public class ServiceManager : IServiceManager
{
    private readonly Lazy<IHotelService> _lazyHotelService;
    private readonly Lazy<IRoomService> _lazyRoomService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyHotelService = new Lazy<IHotelService>(() => new HotelService(repositoryManager)) 
            ?? throw new ArgumentNullException(nameof(repositoryManager));
        _lazyRoomService = new Lazy<IRoomService>(() => new RoomService(repositoryManager)) 
            ?? throw new ArgumentNullException(nameof(repositoryManager));
    }

    public IHotelService HotelService => _lazyHotelService.Value;

    public IRoomService RoomService => _lazyRoomService.Value;
}
