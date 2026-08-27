using MeetHub.Domain.Enums;

namespace MeetHub.Application.DTOs.UserDto
{
    public class LoginResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
