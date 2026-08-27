using System;
using System.Collections.Generic;
using System.Text;

namespace MeetHub.Application.DTOs.UserDto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
