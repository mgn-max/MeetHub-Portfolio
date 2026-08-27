using MeetHub.API.Models.FirmRequest;
using MeetHub.Application.DTOs.FirmDto;

namespace MeetHub.API.Mapper.FirmsMapper
{
    public static class FirmRequestMapper
    {
        public static CreateFirmDto ToDto(this CreateFirmRequest request)
        {
            return new CreateFirmDto
            {
                Id = request.Id,
                CorporateReason = request.CorporateReason,
                FantasyName = request.FantasyName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Cnpj = request.Cnpj,
                IsActive = request.IsActive,
                LogoUrl = request.LogoUrl,
                ZipCode = request.ZipCode,
                Country = request.Country,
                State = request.State,
                City = request.City,
                Neighborhood = request.Neighborhood,
                Street = request.Street,
                AddressNumber = request.AddressNumber,
                CreatedAt = request.CreatedAt
            };
        }
    }
}
