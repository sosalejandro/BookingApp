using BookingApp.DomainLayer.Exceptions;
using BookingApp.DomainLayer.Interfaces;
using BookingApp.DomainLayer.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.DomainLayer.Models;

[MetadataType(typeof(IBaseHotel))]
public class Hotel 
    : BaseEntity, IBaseHotel, IPhotoCollection, IRoomCollection
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public ICollection<Photo>? Photos { get; set; }
    public string? Description { get; set; }
    public int? Rating { get; set; }
    public ICollection<Room>? Rooms { get; set; }
    public long? CheapestPrice { get; set; }
    public long? ExpensiestPrice { get; set; }
    public bool? Featured { get; set; }

    public void AddRoom(Room room)
    {
        if (room is null)
            throw new EmptyRoomException();

        Rooms.Add(room);
    }
 }

