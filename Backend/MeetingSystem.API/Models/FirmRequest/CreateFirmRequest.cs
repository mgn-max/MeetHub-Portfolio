namespace MeetHub.API.Models.FirmRequest
{
    public class CreateFirmRequest
    {
        public Guid Id { get; set; }
        public string CorporateReason { get; set; } = string.Empty;
        public string FantasyName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Cnpj { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsActive { get; set; }
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? AddressNumber { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Country { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
