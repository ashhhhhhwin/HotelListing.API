using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class HotelConfiguration: IEntityTypeConfiguration<Hotel>
    { 
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                 new Hotel
                 {
                     Id = 1,
                     Name = "Taj India",
                     Address = "Mumbai",
                     CountryId = 1,
                     Rating = 4.8
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
                }
                );
                
        }
    }
}
