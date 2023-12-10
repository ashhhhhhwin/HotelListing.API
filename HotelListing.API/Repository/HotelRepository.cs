using HotelListing.API.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {

    
        //Sending the DBContext to the Generic Repository (So that, its constructor can be invoked)
        public HotelRepository(HotelListingDBContext hotelListingDBContext) : base(hotelListingDBContext)
        {
        }
    }
}
