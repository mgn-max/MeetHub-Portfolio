using MeetHub.API.Models.FirmMembershipRequest;
using MeetHub.Application.DTOs.FirmMembershipDto;
using MeetHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeetHub.API.Controllers
{
    [ApiController]
    [Route("api/memberships")]
    public class FirmMembershipController : ControllerBase
    {
        private readonly IFirmMembershipService _firmMembershipService;

        public FirmMembershipController(IFirmMembershipService firmMembershipService)
        {
            _firmMembershipService = firmMembershipService;
        }

        [HttpPost]
        public async Task<ActionResult<FirmMembershipDetailsDto>> CreateMembership([FromBody] CreateMembershipRequest request)
        {
            var membership = await _firmMembershipService.CreateMembership(request.UserId, request.FirmId, request.Profile, request.RequestingProfile);
            return CreatedAtAction(nameof(GetMembershipDetails), new { id = membership.Id }, membership);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FirmMembershipDto>> GetMembership(Guid id)
        {
            var membership = await _firmMembershipService.GetMembershipById(id);
            return Ok(membership);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<FirmMembershipDetailsDto>> GetMembershipDetails(Guid id)
        {
            var membership = await _firmMembershipService.GetMembershipDetailsById(id);
            return Ok(membership);
        }

        [HttpGet("users/{userId}/firms/{firmId}")]
        public async Task<ActionResult<FirmMembershipDto>> GetMembershipByUserAndFirm(Guid userId, Guid firmId)
        {
            var membership = await _firmMembershipService.GetMembershipByUserAndFirm(userId, firmId);
            return Ok(membership);
        }

        [HttpGet("firms/{firmId}")]
        public async Task<ActionResult<List<FirmMembershipDto>>> GetMembershipsByFirm(Guid firmId)
        {
            var memberships = await _firmMembershipService.GetMembershipsByFirmId(firmId);
            return Ok(memberships);
        }

        [HttpGet("users/{userId}")]
        public async Task<ActionResult<List<FirmMembershipDto>>> GetMembershipsByUser(Guid userId)
        {
            var memberships = await _firmMembershipService.GetMembershipsByUserId(userId);
            return Ok(memberships);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateMembership(Guid id, [FromBody] UpdateMembershipRequest request)
        {
            await _firmMembershipService.UpdateMembership(id, request.Profile, request.Status, request.RequestingProfile);
            return NoContent();
        }
    }
}
