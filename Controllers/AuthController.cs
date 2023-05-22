using Employee_management_System_API.Models.DTOs;
using Employee_management_System_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employee_management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await userManager.CreateAsync(IdentityUser, registerRequestDto.Password);
            if(identityResult.Succeeded)
            {
                // Add Roles to this User
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(IdentityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User has been registered successfully. Please Login.");
                    }

                }
               
            }
            return BadRequest("Something Went Wrong");
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> login([FromBody] LoginRequestDto loginRequestDto)
        {
           //looking for the user existence
            var user = await userManager.FindByEmailAsync(loginRequestDto.userName);

            if(user != null)
            {
               var checkPasswordResult =  await userManager.CheckPasswordAsync(user, loginRequestDto.password);
                if (checkPasswordResult)
                {
                    //Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    
                    if(roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                    //returning the jwToken in response instead of passing directly into the ok response 
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
