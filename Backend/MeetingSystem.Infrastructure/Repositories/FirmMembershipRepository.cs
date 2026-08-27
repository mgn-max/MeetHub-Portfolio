using MeetHub.Domain.Entities;
using MeetHub.Domain.Enums.FirmMembershipEnum;
using MeetHub.Domain.Interfaces;
using MeetHub.Domain.Projections;
using MeetHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MeetHub.Infrastructure.Repositories
{
    public class FirmMembershipRepository : IFirmMembershipRepository
    {
        private readonly MeetHubDbContext _context;


        public FirmMembershipRepository(MeetHubDbContext context)
        {
            _context = context;
        }

        public async Task Add(FirmMembership membership)
        {
            await _context.FirmMemberships.AddAsync(membership);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountSuperAdminsByFirmId(Guid firmId)
        {
            return await _context.FirmMemberships.AsNoTracking().CountAsync(m => m.FirmId == firmId && m.Profile == MembershipProfile.SuperAdmin);
        }

        public async Task<bool> ExistsByUserAndFirm(Guid userId, Guid firmId)
        {
            return await _context.FirmMemberships.AsNoTracking().AnyAsync(m => m.UserId == userId && m.FirmId == firmId);
        }

        public async Task<List<FirmMembership>> GetByFirmId(Guid firmId)
        {
            return await _context.FirmMemberships.AsNoTracking().Where(m => m.FirmId == firmId).OrderByDescending(m => m.CreatedAt).ToListAsync();
        }

        public async Task<List<FirmMembershipWithDetails>> GetByFirmIdWithDetails(Guid firmId)
        {
            return await _context.FirmMemberships.AsNoTracking()
                .Join(_context.Users, m => m.UserId, u => u.Id, (m, u) => new { Membership = m, User = u }).AsNoTracking()
                .Join(_context.Firms, mu => mu.Membership.FirmId, f => f.Id, (mu, f) => new { mu.Membership, mu.User, Firm = f }).AsNoTracking()
                .Where(muf => muf.Membership.FirmId == firmId)
                .Select(muf => new FirmMembershipWithDetails
                {
                    Id = muf.Membership.Id,
                    UserId = muf.Membership.UserId,
                    UserName = muf.User.Name,
                    FirmId = muf.Membership.FirmId,
                    FirmName = muf.Firm.FantasyName,
                    Profile = muf.Membership.Profile,
                    Status = muf.Membership.Status,
                    CreatedAt = muf.Membership.CreatedAt
                }).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<List<FirmMembershipWithDetails>> GetByUserIdWithDetails(Guid userId)
        {
            return await _context.FirmMemberships.AsNoTracking()
                .Join(_context.Users, m => m.UserId, u => u.Id, (m, u) => new { Membership = m, User = u }).AsNoTracking()
                .Join(_context.Firms, mu => mu.Membership.FirmId, f => f.Id, (mu, f) => new { mu.Membership, mu.User, Firm = f }).AsNoTracking()
                .Where(muf => muf.Membership.UserId == userId)
                .Select(muf => new FirmMembershipWithDetails
                {
                    Id = muf.Membership.Id,
                    UserId = muf.Membership.UserId,
                    UserName = muf.User.Name,
                    FirmId = muf.Membership.FirmId,
                    FirmName = muf.Firm.FantasyName,
                    Profile = muf.Membership.Profile,
                    Status = muf.Membership.Status,
                    CreatedAt = muf.Membership.CreatedAt
                }).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<FirmMembership?> GetById(Guid id)
        {
            return await _context.FirmMemberships.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<FirmMembership?> GetByUserAndFirm(Guid userId, Guid firmId)
        {
            return await _context.FirmMemberships.AsNoTracking().FirstOrDefaultAsync(m => m.UserId == userId && m.FirmId == firmId);
        }

        public async Task<List<FirmMembership>> GetByUserId(Guid userId)
        {
            return await _context.FirmMemberships.AsNoTracking().Where(m => m.UserId == userId).OrderByDescending(m => m.CreatedAt).ToListAsync();
        }

        public async Task Update(FirmMembership membership)
        {
            _context.FirmMemberships.Update(membership);
            await _context.SaveChangesAsync();
        }
    }
}
