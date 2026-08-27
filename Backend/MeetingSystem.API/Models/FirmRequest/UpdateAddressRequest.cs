namespace MeetHub.API.Models.FirmRequest
{
    public class UpdateAddressRequest
    {
        public string? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? AddressNumber { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; } = string.Empty;
    }
}
