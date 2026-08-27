using MeetHub.Application.DTOs.FirmDto;
using MeetHub.Domain.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetHub.Application.Mapper
{
    public static class FirmAddressDtoMapper
    {
        public static AddressUpdateData ToDto(this UpdateFirmAddressDto request)
        {
            return new AddressUpdateData
            {
                ZipCode = request.ZipCode,
                Country = string.IsNullOrWhiteSpace(request.Country) ? null : request.Country.Trim(),
                State = request.State,
                City = request.City,
                Street = request.Street,
                Neighborhood = request.Neighborhood,
                AddressNumber = request.AddressNumber
            };
        }
    }
}
