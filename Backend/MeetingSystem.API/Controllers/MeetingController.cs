using MeetHub.API.Models.RequestsMeeting;
using MeetHub.Application.DTOs;
using MeetHub.Application.DTOs.MeetingDto;
using MeetHub.Application.Interfaces;
using MeetHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MeetHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateMeetingDto>> CreateMeeting([FromBody] CreateMeetingRequest request)
        {
                var meeting = await _meetingService.CreateMeeting(request.Name, request.CreatorName);
                return CreatedAtAction(nameof(GetMeeting), new { id = meeting.IdMeeting }, meeting);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingDetailsDto>> GetMeeting(Guid id)
        {
                var meeting =  await _meetingService.GetMeetingDetails(id);
                return Ok(meeting);
        }

        [HttpGet("allMeetings")]
        public async Task<ActionResult<List<MeetingListDto>>> GetAllMeetings()
        {
                var meetings = await _meetingService.GetAllMeetings();
                return Ok(meetings);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResultDto<MeetingListDto>>> GetPaged([FromQuery]int pageNumber,[FromQuery] int pageSize)
        {
            var pagedResult = await _meetingService.GetPaged(pageNumber,pageSize);
            return Ok(pagedResult);
        }

        [HttpPatch("{id}/name")]
        public async Task<ActionResult<MeetingListDto>> UpdateMeeting(Guid id, [FromBody] UpdateMeetingRequest request )
        {
                var meeting = await _meetingService.UpdateMeeting(request.Name, id);
                return Ok(meeting);
        }

        [HttpPatch("{id}/finish")]
        public async Task<ActionResult<MeetingListDto>> FinishMeeting(Guid id)
        {
                await _meetingService.FinishMeeting(id);
                return Ok();
        }

        [HttpPost("{idMeeting}/participants")]
        public async Task<ActionResult<MeetingListDto>> AddParticipant(Guid idMeeting, [FromBody] AddParticipantRequest request)
        {
                var response = await _meetingService.AddParticipant(idMeeting, request.ParticipantName);
                return Ok(response);
        }

        [HttpGet("{idMeeting}/participants")]
        public async Task<ActionResult<List<ParticipantDto>>> GetAllParticipants(Guid idMeeting)
        {
                var participants = await _meetingService.GetAllParticipants(idMeeting);
                return Ok(participants);
        }

        [HttpPatch("participants/{idParticipant}/presence")]
        public async Task<ActionResult<ParticipantDto>> UpdateIsPresent(Guid idParticipant)
        {
                await _meetingService.UpdateParticipantIsPresent(idParticipant);
                return Ok();
        }
        [HttpPatch("participants/{idParticipant}/profile")]
        public async Task<ActionResult<ParticipantDto>> UpdateParticipantProfile(Guid idParticipant,[FromBody] AlterProfileParticipantRequest request)
        {
                await _meetingService.UpdateParticipantProfile(idParticipant, request.Profile,request.solicitantProfile);
                return Ok();
        }

        [HttpPatch("participants/{idParticipant}/name")]
        public async Task<ActionResult<ParticipantDto>> UpdateParticipantName(Guid idParticipant,[FromBody] AlterParticipantNameRequest request)
        {
                await _meetingService.UpdateParticipantName(idParticipant, request.Name);
                return Ok();
        }

    }
}