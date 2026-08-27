using MeetHub.Domain.Enums.UserEnum;

namespace MeetHub.Application.DTOs.MeetingDto
{
    public class ParticipantDto
    {
        public Guid IdParticipant { get; set; }
        public string ParticipantName { get; set; } = string.Empty;
        public bool IsPresent { get; set; }
        public PermissionProfile Profile{ get; set; }
    }
}
