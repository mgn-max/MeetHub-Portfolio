using MeetHub.API.Models.RequestsAuth;
using MeetHub.Application.DTOs.UserDto;
using MeetHub.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetHub.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequest login)
        {
            var auth = await _userService.Login(login.LoginOrEmail, login.Password);
            return Ok(auth);
        }

    }
}
