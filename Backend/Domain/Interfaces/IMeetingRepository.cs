using MeetHub.Domain.Entities;

namespace MeetHub.Domain.Interfaces
{
    public interface IMeetingRepository
    {
        Task Add(Meeting meeting);
        Task<Meeting> GetById(Guid id);
        Task<List<Meeting>> GetAll();
        Task<(List<Meeting> meetings, int totalCount)> GetPaged(int pageNumber, int pageSize);
        Task Update(Meeting meeting);
        Task Update(MeetingParticipant participant);
        Task<Meeting> GetByIdWithParticipants(Guid id);
        Task<List<MeetingParticipant>> GetParticipantsByMeetingId(Guid meetingId);
        Task<MeetingParticipant> GetParticipantById(Guid id);
        Task<MeetingParticipant> GetParticipantByIdWithMeeting(Guid id);
        Task AddMeetingParticipant(MeetingParticipant participant);
    }
}
