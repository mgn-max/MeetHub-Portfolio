using MeetHub.Domain.Entities;
using MeetHub.Domain.Projections;

namespace MeetHub.Domain.Interfaces
{
    public interface IFirmMembershipRepository
    {
        Task Add(FirmMembership membership);
        Task<FirmMembership?> GetById(Guid id);
        Task<FirmMembership?> GetByUserAndFirm(Guid userId, Guid firmId);
        Task<List<FirmMembership>> GetByFirmId(Guid firmId);
        Task<List<FirmMembership>> GetByUserId(Guid userId);
        Task<List<FirmMembershipWithDetails>> GetByFirmIdWithDetails(Guid firmId);
        Task<List<FirmMembershipWithDetails>> GetByUserIdWithDetails(Guid userId);
        Task<bool> ExistsByUserAndFirm(Guid userId, Guid firmId);
        Task<int> CountSuperAdminsByFirmId(Guid firmId);
        Task Update(FirmMembership membership);
    }
}
