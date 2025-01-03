using EduPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Authentication;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace EduPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;

        public AuthenticateController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _authenticateService.CheckUserAsync(model.Username);
            if (user != null && await _authenticateService.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _authenticateService.GetRolesAsync(user);
                var token =TokenManager.CreateToken(user, userRoles, _configuration);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _authenticateService.CheckUserAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                UserType = "User"
            };
            var result = await _authenticateService.CreateUserAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (await _authenticateService.RoleExistsAsync(UserRoles.User))
            {
                await _authenticateService.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _authenticateService.CheckUserAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                UserType = "Admin"
            };
            var result = await _authenticateService.CreateUserAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (await _authenticateService.RoleExistsAsync(UserRoles.Admin))
            {
                await _authenticateService.AddToRoleAsync(user, UserRoles.Admin);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}