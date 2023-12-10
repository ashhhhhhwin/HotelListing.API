using System.ComponentModel.DataAnnotations;


namespace HotelListing.API.Models.Hotels
{
    public class BaseHotel
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
