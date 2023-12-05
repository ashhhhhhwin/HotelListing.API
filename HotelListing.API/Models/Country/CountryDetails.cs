using HotelListing.API.Models.Hotels;
namespace HotelListing.API.Models.Country
{
    public class CountryDetails: BaseCountry
    {
        public int Id { get; set; }
        public List<HotelDetails> Hotels { get; set; }

    }
}
