using MeetHub.API.Mapper.FirmsMapper;
using MeetHub.API.Models.FirmRequest;
using MeetHub.Application.DTOs.FirmDto;
using MeetHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace MeetHub.API.Controllers
{
    [ApiController]
    [Route("api/firms")]

    public class FirmController : ControllerBase
    {
        private readonly IFirmService _firmService;

        public FirmController(IFirmService firmService)
        {
            _firmService = firmService;
        }

        [HttpPost]
        public async Task<ActionResult<FirmDetailsDto>> CreateFirm([FromBody] CreateFirmRequest request)
        {
            var firm = await _firmService.CreateFirm(request.ToDto());
            return CreatedAtAction(nameof(GetFirmDetails), new { id = firm.Id }, firm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FirmDto>> GetFirm(Guid id)
        {
            var firm = await _firmService.GetFirmById(id);
            return Ok(firm);
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<FirmDetailsDto>> GetFirmDetails(Guid id)
        {
            var firm = await _firmService.GetFirmDetailsById(id);
            return Ok(firm);
        }

        [HttpPatch("{id}/corporate-reason")]
        public async Task<ActionResult> UpdateCorporateReason(Guid id, [FromBody] UpdateCorporateReasonRequest request)
        {
            await _firmService.UpdateCorporateReason(id, request.CorporateReason);
            return NoContent();
        }

        [HttpPatch("{id}/fantasy-name")]
        public async Task<ActionResult> UpdateFantasyName(Guid id, [FromBody] UpdateFantasyNameRequest request)
        {
            await _firmService.UpdateFantasyName(id, request.FantasyName);
            return NoContent();
        }

        [HttpPatch("{id}/email")]
        public async Task<ActionResult> UpdateEmail(Guid id, [FromBody] UpdateEmailRequest request)
        {
            await _firmService.UpdateEmail(id, request.Email);
            return NoContent();
        }

        [HttpPatch("{id}/phone")]
        public async Task<ActionResult> UpdatePhoneNumber(Guid id, [FromBody] UpdatePhoneNumberRequest request)
        {
            await _firmService.UpdatePhoneNumber(id, request.PhoneNumber);
            return NoContent();
        }

        [HttpPatch("{id}/cnpj")]
        public async Task<ActionResult> UpdateCNPJ(Guid id, [FromBody] UpdateCNPJRequest request)
        {
            await _firmService.UpdateCNPJ(id, request.Cnpj);
            return NoContent();
        }

        [HttpPatch("{id}/logo")]
        public async Task<ActionResult> UpdateLogoUrl(Guid id, [FromBody] UpdateLogoUrlRequest request)
        {
            await _firmService.UpdateLogoUrl(id, request.LogoUrl);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
        {
            await _firmService.UpdateStatus(id, request.Status);
            return NoContent();
        }

        [HttpPatch("{id}/address")]
        public async Task<ActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressRequest request)
        {
            await  _firmService.UpdateAddress(id,request.ToDto());
            return NoContent();
        }
    }
}
