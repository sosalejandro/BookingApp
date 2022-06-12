namespace BookingApp.DomainLayer.Options;
public class RoomParameters : PagingParameters
{
    public RoomParameters()
    {
        OrderBy = "name";
    }
    public uint MinCapacity { get; set; }
    public uint MaxCapacity { get; set; } = int.MaxValue;

    public uint MinPrice { get; set; }
    public uint MaxPrice { get; set; } = int.MaxValue;
    public bool ValidRoomRange => 
        MaxCapacity > MinCapacity;
    public bool ValidRoomPrice => 
        MaxPrice > MinPrice;

    public string? SearchTerm { get; set; }
}
