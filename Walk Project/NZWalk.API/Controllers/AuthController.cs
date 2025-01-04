using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenReositry tokenReositry;

        public AuthController(UserManager<IdentityUser> userManager, ITokenReositry tokenReositry)
        {
            this.userManager = userManager;
            this.tokenReositry = tokenReositry;
        }
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName,
            };

            var identityResult = await userManager.CreateAsync(identityUser,registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                // Add Rols to this User
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Registered! Please Login.");
                    }
                }

            }
            return BadRequest("Somthing want wrong");
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user == null)
            {
                return BadRequest("User name or Password is incorrect");
            }
            var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if(checkPasswordResult)
            {
                // Get Rols for this User
                var roles = await userManager.GetRolesAsync(user);
                
                if (roles != null)
                {
                    // Creat Token
                    var JwtToken = tokenReositry.CreatJwtToken(user, roles.ToList());

                    var response = new LoginResponseDto
                    {
                        JwtToken = JwtToken,
                    };
                    return Ok(response);
                }
                return Ok();
            }
            return BadRequest("User name or Password is incorrect");
        }
    }
}
