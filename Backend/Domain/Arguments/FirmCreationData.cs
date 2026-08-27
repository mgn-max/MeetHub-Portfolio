using System;
using System.Collections.Generic;
using System.Text;

namespace MeetHub.Domain.Arguments
{
    public class FirmCreationData
    {
        public string CorporateReason { get; set; } = string.Empty;
        public string FantasyName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Cnpj { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }

        #region [address]
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? AddressNumber { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Country { get; set; } = string.Empty;
        #endregion
    }
}
