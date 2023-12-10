using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountryRepository:GenericRepository<Country>,ICountryRepository
    {
        private readonly HotelListingDBContext _context;
        public CountryRepository(HotelListingDBContext context):base(context)
        {
            _context= context;
        }

        public async Task<Country> GetDetails(int id)
        {
            //throw new NotImplementedException();
            return await _context.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id);
        }   
    }
}
