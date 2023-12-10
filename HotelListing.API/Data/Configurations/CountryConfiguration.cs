﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class CountryConfiguration:IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                 new Country
                {
                    Id = 1,
                    Name = "India",
                    ShortName = "IND"
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
                });
        }
    }
}
