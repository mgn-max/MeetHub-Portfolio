using MeetHub.Application.DTOs.FirmDto;
using MeetHub.Application.Interfaces;
using MeetHub.Application.Mapper;
using MeetHub.Domain.Entities;
using MeetHub.Domain.Exceptions;
using MeetHub.Domain.Interfaces;

namespace MeetHub.Application.Services
{
    public class FirmService : IFirmService
    {
        private readonly IFirmRepository _firmRepository;

        public FirmService(IFirmRepository firmRespository)
        {
            _firmRepository = firmRespository;
        }

        public async Task<FirmDetailsDto> CreateFirm(CreateFirmDto createFirmDto)
        {
            if (!string.IsNullOrWhiteSpace(createFirmDto.Cnpj) && await _firmRepository.ExistsByCnpj(createFirmDto.Cnpj))
                throw new BusinessRuleException("Não é possivel cadastrar esse CNPJ pois ele já está em uso");

            var firm = new Firm(FirmDtoMapper.ToDto(createFirmDto));
            await _firmRepository.Add(firm);
            return new FirmDetailsDto
            {
                Id = firm.Id,
                CorporateReason = firm.CorporateReason,
                FantasyName = firm.FantasyName,
                Email = firm.Email,
                PhoneNumber = firm.PhoneNumber,
                LogoUrl = firm.LogoUrl,
                Cnpj = firm.Cnpj,
                ZipCode = firm.ZipCode,
                Country = firm.Country,
                State = firm.State,
                City = firm.City,
                Neighborhood = firm.Neighborhood,
                Street = firm.Street,
                AddressNumber = firm.AddressNumber,
                CreatedAt = firm.CreatedAt,
                IsActive = firm.IsActive
            };
        }

        private async Task<Firm> GetSupportFirmById(Guid id)
        {
            var firm = await _firmRepository.GetById(id);
            if (firm == null)
                throw new BusinessRuleException("Empresa não encontrada");
            return firm;
        }

        public async Task<FirmDto> GetFirmById(Guid firmId)
        {
            var firm = await GetSupportFirmById(firmId);
            return new FirmDto
            {
                Id = firm.Id,
                CorporateReason = firm.CorporateReason,
                FantasyName = firm.FantasyName
            };
        }

        public async Task<FirmDetailsDto> GetFirmDetailsById(Guid firmId)
        {
            var firm = await GetSupportFirmById(firmId);
            return new FirmDetailsDto
            {
                Id = firm.Id,
                CorporateReason = firm.CorporateReason,
                FantasyName = firm.FantasyName,
                Email = firm.Email,
                PhoneNumber = firm.PhoneNumber,
                LogoUrl = firm.LogoUrl,
                Cnpj = firm.Cnpj,
                ZipCode = firm.ZipCode,
                Country = firm.Country,
                State = firm.State,
                City = firm.City,
                Neighborhood = firm.Neighborhood,
                Street = firm.Street,
                AddressNumber = firm.AddressNumber,
                CreatedAt = firm.CreatedAt,
                IsActive = firm.IsActive
            };
        }

        public async Task UpdateAddress(Guid id, UpdateFirmAddressDto data)
        {
            var firm = await GetSupportFirmById(id);
            firm.UpdateAddress(FirmAddressDtoMapper.ToDto(data));
            await _firmRepository.Update(firm);
        }

        public async Task UpdateCNPJ(Guid id, string? cnpj)
        {
            var firm = await GetSupportFirmById(id);

            if (!string.IsNullOrWhiteSpace(cnpj) && await _firmRepository.ExistsByCnpjExceptId(cnpj,id))
                throw new BusinessRuleException("Não é possivel cadastrar esse CNPJ pois ele já está em uso");

            firm.UpdateCNPJ(cnpj);
            await _firmRepository.Update(firm);
        }

        public async Task UpdateCorporateReason(Guid id, string corporateReason)
        {
            var firm = await GetSupportFirmById(id);
            firm.UpdateCorporateReason(corporateReason);
            await _firmRepository.Update(firm);
        }

        public async Task UpdateEmail(Guid id, string? email)
        {
            var firm = await GetSupportFirmById(id);
            firm.UpdateEmail(email);
            await _firmRepository.Update(firm);
        }

        public async Task UpdateFantasyName(Guid id, string fantasyName)
        {
            var firm = await GetSupportFirmById(id);
            firm.UpdateFantasyName(fantasyName);
            await _firmRepository.Update(firm);
        }

        public async Task UpdateLogoUrl(Guid id, string? logo)
        {
            var firm = await GetSupportFirmById(id);
            firm.UpdateLogoUrl(logo);
            await _firmRepository.Update(firm);
        }

        public async Task UpdatePhoneNumber(Guid id, string? phoneNumber)
        {
            var firm = await GetSupportFirmById(id);
            firm.UpdatePhoneNumber(phoneNumber);
            await _firmRepository.Update(firm);
        }

        public async Task UpdateStatus(Guid id, bool status)
        {
            var firm = await GetSupportFirmById(id);
            firm.UpdateStatus(status);
            await _firmRepository.Update(firm);
        }
    }
}
