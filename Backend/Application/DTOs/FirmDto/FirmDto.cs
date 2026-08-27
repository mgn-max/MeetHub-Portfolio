using System;
using System.Collections.Generic;
using System.Text;

namespace MeetHub.Application.DTOs.FirmDto
{
    public class FirmDto
    {
        public Guid Id { get; set; }
        public string CorporateReason { get; set; } = string.Empty;
        public string FantasyName { get; set; } = string.Empty;
    }
}
