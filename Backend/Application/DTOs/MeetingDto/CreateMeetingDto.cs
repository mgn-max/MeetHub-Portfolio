namespace MeetHub.Application.DTOs.MeetingDto
{
    public class CreateMeetingDto
    { 
        public Guid IdMeeting { get; set; }
        public string MeetingName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

    }
}
