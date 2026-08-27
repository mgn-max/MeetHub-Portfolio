using MeetHub.Application.DTOs;
using MeetHub.Application.DTOs.MeetingDto;
using MeetHub.Domain.Entities;
using MeetHub.Domain.Enums.UserEnum;

namespace MeetHub.Application.Interfaces
{
    public interface IMeetingService
    {
        Task<CreateMeetingDto> CreateMeeting(string name, string creatorName);
        Task<MeetingDetailsDto> GetMeetingDetails(Guid id);
        Task<List<MeetingListDto>> GetAllMeetings();
        Task<PagedResultDto<MeetingListDto>> GetPaged(int pageNumber, int pageSize);
        Task<MeetingListDto> UpdateMeeting(string name,Guid id);
        Task FinishMeeting(Guid id);
        Task<AddParticipantResponseDto> AddParticipant(Guid meetingId, string participantName);
        Task<List<ParticipantDto>> GetAllParticipants(Guid id);
        Task UpdateParticipantIsPresent(Guid id);
        Task UpdateParticipantProfile(Guid id, PermissionProfile profile, PermissionProfile solicitantProfile);
        Task UpdateParticipantName(Guid id, string name);
    }
}