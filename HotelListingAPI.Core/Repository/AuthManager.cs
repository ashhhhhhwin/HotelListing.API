using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _userManager;
        private readonly IConfiguration _configuration;
        private APIUser _user;
        private const string _loginProvider = "HotelListingAPI";
        private const string _refreshToken = "RefreshToken";
        public AuthManager(IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            _configuration = configuration;

        }
        public async Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO)
        {
            _user = _mapper.Map<APIUser>(userDTO);
            _user.UserName = userDTO.Email;
            var result = await _userManager.CreateAsync(_user, userDTO.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }
            return result.Errors;
        }
        public async Task<IEnumerable<IdentityError>> RegisterAdmin(APIUserDTO userDTO)
        {
            _user = _mapper.Map<APIUser>(userDTO);
            _user.UserName = userDTO.Email;
            var result = await _userManager.CreateAsync(_user, userDTO.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "ADMIN");
            }
            return result.Errors;
        }
        public async Task<AuthResponseDTO> Login(LoginDTO loginDTO)
        {

            _user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (_user == null)
            {
                return null;

            }
            var token = await GenerateToken();

            return new AuthResponseDTO
            {
                Token = token,
                UserId = _user.Id,
                RefershToken = await CreateRefreshToken()
            };
        }
        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub,_user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,_user.Email),
                new Claim("uid",_user.Id)
                }
            .Union(userClaims).Union(roleClaims);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var results = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO responseDTO)
        {
            //Verifies the refresh token
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(responseDTO.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault
                (q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);
            if (_user == null || _user.Id != responseDTO.UserId) {
                return null;
            }
            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, responseDTO.RefershToken);
            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDTO
                {
                    Token = token,
                    UserId = _user.Id,
                    RefershToken = await CreateRefreshToken()
                };
            }
            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
    }
            
}
    

