using MeetHub.Infrastructure.Data;
using MeetHub.Domain.Entities;
using MeetHub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetHub.Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MeetHubDbContext _context;
        public MeetingRepository(MeetHubDbContext meeting)
        {
            _context = meeting;
        }
        public async Task Add(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();
        }

        public async Task <List<Meeting>> GetAll()
        {
            return await _context.Meetings.AsNoTracking().ToListAsync();
        }

        public async Task<Meeting> GetById(Guid id)
        {
            return await _context.Meetings.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<(List<Meeting> meetings, int totalCount)> GetPaged(int pageNumber, int pageSize)
        {
            var meetings = await _context.Meetings.AsNoTracking().OrderByDescending(m => m.CreatedAt).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalCount = await _context.Meetings.AsNoTracking().CountAsync();
            return (meetings, totalCount);
        }

        public async Task Update(Meeting meeting)
        {
            _context.Meetings.Update(meeting);
            await _context.SaveChangesAsync();
        }
        public async Task Update(MeetingParticipant participant)
        {
            _context.MeetingParticipants.Update(participant);
            await _context.SaveChangesAsync();
        }

        public async Task<Meeting> GetByIdWithParticipants(Guid id)
        {
            return await _context.Meetings.Include(m => m.MeetingParticipants).FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<List<MeetingParticipant>> GetParticipantsByMeetingId(Guid meetingId)
        {
            return await _context.MeetingParticipants.AsNoTracking().Where(mp => mp.MeetingId == meetingId).ToListAsync();
        }
        public async Task<MeetingParticipant> GetParticipantByIdWithMeeting(Guid id)
        {
            return await _context.MeetingParticipants.AsNoTracking().Include(mp => mp.Meeting).FirstOrDefaultAsync(mp => mp.Id == id);
        }
        public async Task<MeetingParticipant> GetParticipantById(Guid id)
        {
            return await _context.MeetingParticipants.AsNoTracking().FirstOrDefaultAsync(mp => mp.Id == id);
        }

        public async Task AddMeetingParticipant(MeetingParticipant participant)
        {
            await _context.MeetingParticipants.AddAsync(participant);
            await _context.SaveChangesAsync();
        }
    }
}
