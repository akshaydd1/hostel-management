using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HostelManagementApi.DTOs.Requests
{
    public class CreateRentCollectionRequest
    {
        [Required(ErrorMessage = "Student ID is required.")]
        [JsonPropertyName("student_id")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Room ID is required.")]
        [JsonPropertyName("room_id")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Total rent is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total rent must be greater than 0.")]
        [JsonPropertyName("total_rent")]
        public decimal TotalRent { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Paid rent must be 0 or greater.")]
        [JsonPropertyName("paid_rent")]
        public decimal PaidRent { get; set; } = 0;
    }
}
