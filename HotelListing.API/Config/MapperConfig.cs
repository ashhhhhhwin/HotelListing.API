using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotels;
using HotelListing.API.Models.Users;

namespace HotelListing.API.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, AddCountry>().ReverseMap();
            CreateMap<Country, GetCountryDetails>().ReverseMap();
            CreateMap<Country, CountryDetails>().ReverseMap();
            CreateMap<Hotel, HotelDetails>().ReverseMap();
            CreateMap<Country, UpdateCountry>().ReverseMap();
            //CreateMap<Hotel, HotelDetails>().ReverseMap();
            CreateMap<Hotel, AddHotel>().ReverseMap();
            CreateMap<Hotel, GetHotelDetails>().ReverseMap();
            CreateMap<Hotel, UpdateHotel>().ReverseMap();
            CreateMap<APIUser,APIUserDTO>().ReverseMap();
        }
    }
}
