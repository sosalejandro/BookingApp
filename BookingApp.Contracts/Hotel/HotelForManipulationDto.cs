using BookingApp.DomainLayer.Interfaces;
using BookingApp.DomainLayer.Models;
using BookingApp.DomainLayer.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.Contracts.Hotel;

[MetadataType(typeof(IBaseHotel))]
public abstract record HotelForManipulationDto : IBaseHotel, IPhotoCollection
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public int? Rating { get; set; }
    public long? CheapestPrice { get; set; }
    public long? ExpensiestPrice { get; set; }
    public bool? Featured { get; set; }
    public ICollection<Photo>? Photos { get; set; }
}
