using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Range(1, 5)]
    public uint MinRating { get; set; }
    [Range(1, 5)]
    public uint MaxRating { get; set; } = 5;
    public bool ValidPriceRange => MaxPrice >= MinPrice;
    public bool ValidRatingRange => MaxRating >= MinRating;
    public string? SearchTerm { get; set; }
}
