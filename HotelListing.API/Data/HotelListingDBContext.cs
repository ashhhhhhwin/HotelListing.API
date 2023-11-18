using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelListing.API.Data
{
    public class HotelListingDBContext:DbContext
    {
        public HotelListingDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id=1,
                    Name="India",
                    ShortName="IND"
                },
                new Country
                {
                    Id = 2,
                    Name = "Australia",
                    ShortName = "AUS"
                },
                new Country
                {
                    Id = 3,
                    Name = "China",
                    ShortName = "CHN"
                },
                new Country
                {
                    Id = 4,
                    Name = "United States of America",
                    ShortName = "USA"
                },
                new Country
                {
                    Id = 5,
                    Name = "Russia",
                    ShortName = "RUS"
                }

                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id=1,
                    Name="Taj India",
                    Address="Mumbai",
                    CountryId=1,
                    Rating=4.8
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Taj USA",
                    Address = "Chicago",
                    CountryId = 4,
                    Rating = 4.6
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Taj Russia",
                    Address = "Moscow",
                    CountryId = 5,
                    Rating = 4.2
                });

        }
    }
}
