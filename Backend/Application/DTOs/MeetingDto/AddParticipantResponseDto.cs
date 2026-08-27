namespace MeetHub.Application.DTOs.MeetingDto
{
    public class AddParticipantResponseDto
    {
        public Guid MeetingId { get; set; }
        public String ParticipantName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
