namespace HostelManagementApi.DTOs.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? DocType { get; set; }
        public string? DocNumber { get; set; }
        public string? MobileNo { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
