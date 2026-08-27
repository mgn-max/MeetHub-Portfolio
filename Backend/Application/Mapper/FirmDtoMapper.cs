using MeetHub.Application.DTOs.FirmDto;
using MeetHub.Domain.Arguments;

namespace MeetHub.Application.Mapper
{
    public static class FirmDtoMapper
    {
        public static FirmCreationData ToDto(this CreateFirmDto request)
        {
            return new FirmCreationData
            {
                CorporateReason = request.CorporateReason,
                FantasyName = request.FantasyName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Cnpj = request.Cnpj,
                LogoUrl = request.LogoUrl,
                ZipCode = request.ZipCode,
                Country = request.Country,
                State = request.State,
                City = request.City,
                Street = request.Street,
                Neighborhood = request.Neighborhood,
                AddressNumber = request.AddressNumber
            };
        }
    }
}
