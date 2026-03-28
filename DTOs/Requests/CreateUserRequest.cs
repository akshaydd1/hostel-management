using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HostelManagementApi.DTOs.Requests
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        public string? City { get; set; }
        public string? State { get; set; }
        public string? DocType { get; set; }
        public string? DocNumber { get; set; }

        [JsonPropertyName("mobile_no")]
        public string? MobileNo { get; set; }
    }
}
