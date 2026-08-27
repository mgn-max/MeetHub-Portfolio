using MeetHub.API.Mapper.UsersMapper;
using MeetHub.API.Models.RequestsUser;
using MeetHub.Application.DTOs.UserDetailsDto;
using MeetHub.Application.DTOs.UserDto;
using MeetHub.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetHub.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDetailsDto>> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await _userService.CreateUser(request.ToDto());
            return CreatedAtAction(nameof(GetUserDetails), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails(Guid id)
        {
            var user = await _userService.GetUserDetailsById(id);
            return Ok(user);
        }

        [HttpPatch("{id}/name")]
        public async Task<ActionResult> UpdateName(Guid id, [FromBody] UpdateNameRequest request)
        {
            await _userService.UpdateUserName(id, request.Name);
            return NoContent();
        }

        [HttpPatch("{id}/display")]
        public async Task<ActionResult> UpdateDisplayName(Guid id, [FromBody] UpdateUserDisplayNameRequest request)
        {
            await _userService.UpdateUserDisplayName(id, request.DisplayName);
            return NoContent();
        }

        [HttpPatch("{id}/login")]
        public async Task<ActionResult> UpdateLogin(Guid id, [FromBody] UpdateLoginRequest request)
        {
            await _userService.UpdateUserLogin(id, request.Login);
            return NoContent();
        }

        [HttpPatch("{id}/password")]
        public async Task<ActionResult> UpdatePassword(Guid id, [FromBody] UpdatePasswordRequest request)
        {
            await _userService.UpdateUserPassword(id, request.Password);
            return NoContent();
        }

        [HttpPatch("{id}/email")]
        public async Task<ActionResult> UpdateEmail(Guid id, [FromBody] UpdateEmailRequest request)
        {
            await _userService.UpdateUserEmail(id, request.Email);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateUserStatus(Guid id, [FromBody] UpdateStatusRequest request)
        {
            await _userService.UpdateUserStatus(id, request.UserStatus);
            return NoContent();
        }

        [HttpPatch("{id}/photo")]
        public async Task<ActionResult> UpdateUserPhoto(Guid id, [FromBody] UpdatePhotoRequest request)
        {
            await _userService.UpdateUserPhoto(id, request.PhotoUrl);
            return NoContent();
        }
    }
}
