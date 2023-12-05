using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelListingDBContext _dbContext;
        public GenericRepository(HotelListingDBContext hotelListingDBContext)
        {
            _dbContext= hotelListingDBContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        
        public async Task DeleteAsync(int? id)
        {
            var entity1 =await GetAsync(id);
            _dbContext.Set<T>().Remove(entity1);
           // return entity1;
        }

        public async Task<bool> Exists(int? id)
        {
            var entity = await GetAsync(id);
            if(entity == null)
            {
                return false;
            }
            return true;
;        }

        public async Task<List<T>> GetAllAsync()
        {
           return await _dbContext.Set<T>().ToListAsync();
           

        }

        public async Task<T> GetAsync(int? id)
        {
            if(id==null)
            {
                return null;
            }
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
