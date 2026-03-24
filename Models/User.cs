namespace HostelManagementApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? DocType { get; set; }
        public string? DocNumber { get; set; }
        public string? MobileNo { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
