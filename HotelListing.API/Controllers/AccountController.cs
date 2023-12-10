using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            this._authManager = authManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody]APIUserDTO aPIUserDTO)
        {
            var errors = await _authManager.Register(aPIUserDTO);
            if(errors.Any())
            {
                foreach(var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);

                }
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<ActionResult> RegisterAdmin([FromBody]APIUserDTO aPIUserDTO)
        {
            var errors = await _authManager.RegisterAdmin(aPIUserDTO);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);

                }
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var valid=await _authManager.Login(loginDTO);
            if(valid==null)
            {
                return Unauthorized();
            }
            return Ok(valid);
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDTO authResponseDTO)
        {
            var valid = await _authManager.VerifyRefreshToken(authResponseDTO);
            if (valid == null)
            {
                return Unauthorized();
            }
            return Ok(valid);
        }


    }
}
