using BookingApp.DomainLayer.ValueObjects;

namespace BookingApp.DomainLayer.Interfaces;
public interface IPhotoCollection
{
    public ICollection<Photo>? Photos { get; set; }
}
