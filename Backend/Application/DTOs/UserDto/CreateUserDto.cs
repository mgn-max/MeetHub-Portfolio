using MeetHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeetHub.Application.DTOs.UserDto
{
    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string? DisplayName { get; set; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhotoUrl {  get; set; }
    }
}
