using AuthService.Application.DTO;
using AuthService.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public AuthController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = _userAppService.LoginUser(loginDTO);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] SignUpDTO signUpDTO)
        {
            var isRegistered = _userAppService.RegisterUser(signUpDTO);
            if (isRegistered)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("User already exists");
            }
        }

    }
}
