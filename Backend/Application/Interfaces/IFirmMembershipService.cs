using MeetHub.Application.DTOs.FirmMembershipDto;
using MeetHub.Domain.Enums.FirmMembershipEnum;

namespace MeetHub.Application.Interfaces
{
    public interface IFirmMembershipService
    {
        Task<FirmMembershipDetailsDto> CreateMembership(
            Guid userId,
            Guid firmId,
            MembershipProfile profile,
            MembershipProfile requestingProfile);
        Task<FirmMembershipDto> GetMembershipById(Guid id);
        Task<FirmMembershipDetailsDto> GetMembershipDetailsById(Guid id);
        Task<FirmMembershipDto> GetMembershipByUserAndFirm(Guid userId, Guid firmId);
        Task<List<FirmMembershipDto>> GetMembershipsByFirmId(Guid firmId);
        Task<List<FirmMembershipDto>> GetMembershipsByUserId(Guid userId);
        Task UpdateMembership(
            Guid id,
            MembershipProfile? profile,
            MembershipStatus? status,
            MembershipProfile requestingProfile);
    }
}
