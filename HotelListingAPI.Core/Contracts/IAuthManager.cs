using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO);
        Task<AuthResponseDTO> Login(LoginDTO loginDTO);
        Task<IEnumerable<IdentityError>> RegisterAdmin(APIUserDTO userDTO);
        Task<string> CreateRefreshToken();
        Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO responseDTO);
        
    }
}
