using MeetHub.API.Models.FirmRequest;
using MeetHub.Application.DTOs.FirmDto;

namespace MeetHub.API.Mapper.FirmsMapper
{
    public static class AddresRequestMapper
    {
        public static UpdateFirmAddressDto ToDto(this UpdateAddressRequest data)
        {
            return new UpdateFirmAddressDto
            {
                ZipCode = data.ZipCode,
                Country = data.Country,
                State = data.State,
                City = data.City,
                Street = data.Street,
                Neighborhood = data.Neighborhood,
                AddressNumber = data.AddressNumber
            };
        }
    }
}
