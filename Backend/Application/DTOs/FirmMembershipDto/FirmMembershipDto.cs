using MeetHub.Domain.Enums.FirmMembershipEnum;

namespace MeetHub.Application.DTOs.FirmMembershipDto
{
    public class FirmMembershipDto
    {
        public Guid Id { get; set; }
        public string FirmName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public Guid FirmId { get; set; }
        public Guid UserId { get; set; }
        public MembershipProfile Profile { get; set; }
        public MembershipStatus Status { get; set; }
    }
}
