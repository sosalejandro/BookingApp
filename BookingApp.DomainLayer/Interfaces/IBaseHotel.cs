using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DomainLayer.Interfaces;
public interface IBaseHotel
{
    [Required(ErrorMessage = "Hotel name is a required field.")]
    [MaxLength(
        60,
        ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Hotel type is a required field.")]
    [MaxLength(
        30,
        ErrorMessage = "Maximum length for the Type is 30 characters.")]
    public string? Type { get; set; }
    [Required(ErrorMessage = "Hotel city is a required field.")]
    [MaxLength(
        60,
        ErrorMessage = "Maximum length for the City is 60 characters.")]
    public string? City { get; set; }
    [Required(ErrorMessage = "Hotel address is a required field.")]
    [MaxLength(
        60,
        ErrorMessage = "Maximum length for the Address is 60 characters.")]
    public string? Address { get; set; }
    [Required(ErrorMessage = "Hotel description is a required field.")]
    [MaxLength(
        180,
        ErrorMessage = "Maximum length for the Address is 180 characters.")]
    public string? Description { get; set; }
    public int? Rating { get; set; }
    public long? CheapestPrice { get; set; }
    public long? ExpensiestPrice { get; set; }
    public bool? Featured { get; set; }
}
