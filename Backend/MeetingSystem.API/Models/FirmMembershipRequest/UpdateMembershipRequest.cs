using MeetHub.Domain.Enums.FirmMembershipEnum;

namespace MeetHub.API.Models.FirmMembershipRequest
{
    public class UpdateMembershipRequest
    {
        public MembershipProfile? Profile{ get; set; }
        public MembershipStatus? Status { get; set; }
        public MembershipProfile RequestingProfile { get; set; }
    }
}
