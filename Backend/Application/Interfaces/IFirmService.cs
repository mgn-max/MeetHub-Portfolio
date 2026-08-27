using MeetHub.Application.DTOs.FirmDto;

namespace MeetHub.Application.Interfaces
{
    public interface IFirmService
    {
        Task<FirmDetailsDto> CreateFirm(CreateFirmDto createFirmDto);
        Task<FirmDto> GetFirmById(Guid firmId);
        Task<FirmDetailsDto> GetFirmDetailsById(Guid firmId);

        #region[updates]
        Task UpdateCorporateReason(Guid id, string corporateReason);
        Task UpdateFantasyName(Guid id, string fantasyName);
        Task UpdateEmail(Guid id, string? email);
        Task UpdatePhoneNumber(Guid id, string? phoneNumber);
        Task UpdateCNPJ(Guid id,string? cnpj);
        Task UpdateLogoUrl(Guid id, string? logo);
        Task UpdateStatus(Guid id, bool status);
        Task UpdateAddress(Guid id, UpdateFirmAddressDto data);
        #endregion
    }
}
