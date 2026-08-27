namespace MeetHub.API.Models.RequestsMeeting
{
    public class CreateMeetingRequest
    {
        public string Name { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
    }
}
