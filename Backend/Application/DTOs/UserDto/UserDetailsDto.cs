using MeetHub.Domain.Enums.UserEnum;

namespace MeetHub.Application.DTOs.UserDetailsDto
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserStatus UserStatus { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
