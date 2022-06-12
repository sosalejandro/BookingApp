using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Options;
public class HotelParameters : PagingParameters
{
    public HotelParameters()
    {
        OrderBy = "name";
    }
    public string? City { get; set; }
    public uint MinPrice { get; set; }
    public uint MaxPrice { get; set; } = int.MaxValue;
    public uint MinRating { get; set; }
    public uint MaxRating { get; set; } = int.MaxValue;
    public bool ValidPriceRange => MinPrice > MaxPrice;
    public bool ValidRatingRange => MinRating > MaxRating;
    public string? SearchTerm { get; set; }
}
