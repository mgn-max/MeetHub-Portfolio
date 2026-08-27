using MeetHub.Domain.Enums.FirmMembershipEnum;

namespace MeetHub.API.Models.FirmMembershipRequest
{
    public class CreateMembershipRequest
    {
        public Guid UserId { get; set; }
        public Guid FirmId { get; set; }
        public MembershipProfile Profile { get; set; }
        public MembershipProfile RequestingProfile { get; set; }
    }
}
