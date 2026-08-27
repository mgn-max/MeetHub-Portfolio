using MeetHub.Domain.Enums.FirmMembershipEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetHub.Domain.Projections
{
    public class FirmMembershipWithDetails
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid FirmId { get; set; }
        public string FirmName { get; set; } = string.Empty;
        public MembershipProfile Profile { get; set; }
        public MembershipStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
