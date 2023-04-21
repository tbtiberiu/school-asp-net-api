using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService authService { get; set; }

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }


        [HttpPost("/register")]
        public IActionResult Register(UserRegisterDto registerData)
        {
            User user = authService.Register(registerData);
            return Ok(user);
        }

        [HttpPost("/login")]
        public IActionResult Login(UserLoginDto loginData)
        {
            if (loginData == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = authService.Login(loginData);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
