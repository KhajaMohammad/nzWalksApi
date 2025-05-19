using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using nzWalksApi.Models.DTO;
using nzWalksApi.Repositories;

namespace nzWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<IdentityUser> userManager;
        public readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository) {
        this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        // POst : http:localhost:/api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {

                // add Roles to this user 

                if ((registerRequestDto.Roles != null && registerRequestDto.Roles.Any()))
                {

                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded) {

                        return Ok("User Registered! Please Login");
                    
                        
                    }

                }


            }
            return BadRequest("Something Went Wrong");
        }

        // post /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto )
        {
         var user =    await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
              bool chechPasswordResult =   await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (chechPasswordResult) {

                    //create token
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                       var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
                        return Ok(new LoginResponseDto { JwtToken = jwtToken});
                    }

                    


                }

            }
            return BadRequest("UserName or Password Incorrect ");
        }
         

    }
     
}
