using MeetHub.Domain.Enums.UserEnum;

namespace MeetHub.API.Models.RequestsUser
{
    public class UpdateStatusRequest
    {
        public UserStatus UserStatus { get; set; }
    }
}
