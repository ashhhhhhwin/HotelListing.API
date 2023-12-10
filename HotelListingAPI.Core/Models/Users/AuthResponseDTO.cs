namespace HotelListing.API.Models.Users
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string RefershToken { get; set; }
    }
}
