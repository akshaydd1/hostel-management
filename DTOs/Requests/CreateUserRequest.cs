using System.ComponentModel.DataAnnotations;

namespace HostelManagementApi.DTOs.Requests
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        public string? City { get; set; }
        public string? State { get; set; }
        public string? DocType { get; set; }
        public string? DocNumber { get; set; }
        public string? MobileNo { get; set; }
    }
}
