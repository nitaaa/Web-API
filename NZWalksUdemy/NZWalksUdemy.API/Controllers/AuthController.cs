using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksUdemy.API.Models.DTO;
using NZWalksUdemy.API.Repositories;

namespace NZWalksUdemy.API.Controllers
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

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.username,
                Email = registerRequestDTO.username
            };
            var identiyResult = await userManager.CreateAsync(identityUser, registerRequestDTO.password);

            if (identiyResult.Succeeded)
            {
                // Add roles to user
                if (registerRequestDTO.roles != null)
                {
                    identiyResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.roles);
                    if (identiyResult.Succeeded)
                    {
                        return Ok("User was successfully registered.");
                    }
                }
                //return Ok("User was successfully registered.");
            }
            return BadRequest("Something went wrong.");
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.username);
            if (user == null)
            {
                var checkresult = await userManager.CheckPasswordAsync(user, loginRequestDTO.password);
                if (checkresult)
                {
                    // Get Roles
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token
                        var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(response);
                    }
                }
            }
            return BadRequest("Incorrect username or password.");
        }
    }
}
