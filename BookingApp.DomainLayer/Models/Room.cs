namespace BookingApp.DomainLayer.Models;

public class Room : BaseEntity
{
    public int HotelId { get; set; }

    public string Name { get; set; }
    public uint Price { get; set; }
    public uint Capacity { get; set; }

    public void SetPrice(uint price)
    {
        Price = price;
    }

    public void SetHotel(int hotelId)
    {
        HotelId = hotelId;
    }
}