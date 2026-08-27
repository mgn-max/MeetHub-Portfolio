using MeetHub.Domain.Enums.UserEnum;

namespace MeetHub.API.Models.RequestsMeeting
{
    public class AlterProfileParticipantRequest
    {
        public PermissionProfile solicitantProfile { get; set; }
        public PermissionProfile Profile { get; set; }
    }
}
