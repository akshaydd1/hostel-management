using System.Text.Json.Serialization;

namespace HostelManagementApi.DTOs.Requests
{
    public class UpdateUserRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? DocType { get; set; }
        public string? DocNumber { get; set; }

        [JsonPropertyName("mobile_no")]
        public string? MobileNo { get; set; }
    }
}
