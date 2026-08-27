using MeetHub.Application.DTOs.FirmMembershipDto;
using MeetHub.Application.Interfaces;
using MeetHub.Domain.Entities;
using MeetHub.Domain.Enums.FirmMembershipEnum;
using MeetHub.Domain.Exceptions;
using MeetHub.Domain.Interfaces;
using MeetHub.Domain.Projections;

namespace MeetHub.Application.Services
{
    public class FirmMembershipService : IFirmMembershipService
    {
        private readonly IFirmMembershipRepository _firmMembershipRepository;
        private readonly IFirmRepository _firmRepository;
        private readonly IUserRepository _userRepository;

        public FirmMembershipService(IFirmMembershipRepository firmMembershipRepository, IFirmRepository firmRepository, IUserRepository userRepository)
        {
            _firmMembershipRepository = firmMembershipRepository;
            _firmRepository = firmRepository;
            _userRepository = userRepository;
        }

        public async Task<FirmMembershipDetailsDto> CreateMembership(Guid userId, Guid firmId, MembershipProfile profile, MembershipProfile requestingProfile)
        {
            if (requestingProfile != MembershipProfile.SuperAdmin && requestingProfile != MembershipProfile.Admin)
                throw new BusinessRuleException("Apenas administradores podem criar associações de membros.");
            if (profile == MembershipProfile.SuperAdmin && requestingProfile != MembershipProfile.SuperAdmin)
                throw new BusinessRuleException("Apenas super administradores podem atribuir o perfil de super administrador.");
            if (await _firmMembershipRepository.ExistsByUserAndFirm(userId, firmId))
                throw new BusinessRuleException("Usuário já possui uma associação com esta empresa.");
            var related = await GetUserAndFirmNames(userId, firmId);

            var membership = new FirmMembership(userId, firmId, profile);
            await _firmMembershipRepository.Add(membership);
            return MapToDetailsDto(membership, related.userName, related.firmName);
        }

        private async Task<FirmMembership> GetSupportMembershipById(Guid id)
        {
            var membership = await _firmMembershipRepository.GetById(id);
            if (membership == null)
                throw new BusinessRuleException("Associação não encontrada");
            return membership;
        }

        public async Task<FirmMembershipDto> GetMembershipById(Guid id)
        {
            var membership = await GetSupportMembershipById(id);

            var related = await GetUserAndFirmNames(membership.UserId, membership.FirmId);

            return MapToDto(membership, related.userName, related.firmName);
        }

        public async Task<FirmMembershipDto> GetMembershipByUserAndFirm(Guid userId, Guid firmId)
        {
            var membership = await _firmMembershipRepository.GetByUserAndFirm(userId, firmId);
            if (membership == null)
                throw new BusinessRuleException("Associação não encontrada");

            var related = await GetUserAndFirmNames(membership.UserId, membership.FirmId);

            return MapToDto(membership, related.userName, related.firmName);
        }

        public async Task<FirmMembershipDetailsDto> GetMembershipDetailsById(Guid id)
        {
            var membership = await GetSupportMembershipById(id);
            var related = await GetUserAndFirmNames(membership.UserId, membership.FirmId);

            return MapToDetailsDto(membership, related.userName, related.firmName);
        }

        public async Task<List<FirmMembershipDto>> GetMembershipsByFirmId(Guid firmId)
        {
            var memberships = await _firmMembershipRepository.GetByFirmIdWithDetails(firmId);
            return memberships.Select(m => MapFromProjection(m)).ToList();
        }

        public async Task<List<FirmMembershipDto>> GetMembershipsByUserId(Guid userId)
        {
            var memberships = await _firmMembershipRepository.GetByUserIdWithDetails(userId);
            return memberships.Select(m => MapFromProjection(m)).ToList();
        }

        public async Task UpdateMembership(Guid id, MembershipProfile? profile, MembershipStatus? status, MembershipProfile requestingProfile)
        {
            var membership = await GetSupportMembershipById(id);
            if (!status.HasValue && !profile.HasValue)
                throw new BusinessRuleException("Nenhuma alteração foi feita na associação.");
            if (profile.HasValue)
                membership.UpdateMembershipProfile(profile.Value, requestingProfile);
            if (status.HasValue)
                membership.UpdateMembershipStatus(status.Value, requestingProfile);

            await _firmMembershipRepository.Update(membership);
        }

        private async Task<(string userName, string firmName)> GetUserAndFirmNames(Guid userId, Guid firmId)
        {
            var firmTask = _firmRepository.GetById(firmId);
            var userTask = _userRepository.GetById(userId);
            await Task.WhenAll(firmTask, userTask);
            var firm = await firmTask;
            var user = await userTask;
            if (firm == null)
                throw new BusinessRuleException("Empresa não encontrada");
            if (user == null)
                throw new BusinessRuleException("Usuário não encontrado");
            return (user.Name, firm.FantasyName);
        }

        private FirmMembershipDto MapToDto(FirmMembership membership, string userName, string firmName)
        {
            return new FirmMembershipDto
            {
                Id = membership.Id,
                FirmName = firmName,
                UserName = userName,
                FirmId = membership.FirmId,
                UserId = membership.UserId,
                Profile = membership.Profile,
                Status = membership.Status
            };
        }

        private FirmMembershipDetailsDto MapToDetailsDto(FirmMembership membership, string userName, string firmName)
        {
            return new FirmMembershipDetailsDto
            {
                Id = membership.Id,
                FirmName = firmName,
                UserName = userName,
                FirmId = membership.FirmId,
                UserId = membership.UserId,
                Profile = membership.Profile,
                Status = membership.Status,
                CreatedAt = membership.CreatedAt
            };
        }

        private FirmMembershipDto MapFromProjection(FirmMembershipWithDetails membership)
        {
            return new FirmMembershipDto
            {
                Id = membership.Id,
                FirmName = membership.FirmName,
                UserName = membership.UserName,
                FirmId = membership.FirmId,
                UserId = membership.UserId,
                Profile = membership.Profile,
                Status = membership.Status
            };
        }
    }
}
