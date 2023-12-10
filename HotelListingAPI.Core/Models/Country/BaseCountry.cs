using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Country
{
    public class BaseCountry
    {
        [Required]
        public string  Name { get; set; }
        public string ShortName { get; set; }
    }
}
