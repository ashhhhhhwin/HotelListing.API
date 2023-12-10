using HotelListing.API.Models.Country;

namespace HotelListing.API.Models.Hotels
{
    public class GetHotelDetails:BaseHotel
    {
        public int Id { get; set; }
        public BaseCountry BaseCountry { get; set; } 
    }
}
