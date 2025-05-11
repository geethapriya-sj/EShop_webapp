using AuthService.Application.DTOs;
using AuthService.Application.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public AuthController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = _userAppService.LoginUser(loginDTO);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("Invalid credentials");
        }

        [HttpPost]
        public IActionResult Register([FromBody] SignUpDTO signUpDTO)
        {
            var result = _userAppService.RegisterUser(signUpDTO);
            if (result)
            {
                return Ok("User registered successfully");
            }
            return BadRequest("User registration failed");
        }       
    }
}
