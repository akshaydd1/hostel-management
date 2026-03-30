using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HostelManagementApi.DTOs.Requests
{
    public class UpdateRentCollectionRequest
    {
        [JsonPropertyName("student_id")]
        public int? StudentId { get; set; }

        [JsonPropertyName("room_id")]
        public int? RoomId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total rent must be greater than 0.")]
        [JsonPropertyName("total_rent")]
        public decimal? TotalRent { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Paid rent must be 0 or greater.")]
        [JsonPropertyName("paid_rent")]
        public decimal? PaidRent { get; set; }

        [RegularExpression("^(pending|partial|paid)$", ErrorMessage = "Status must be 'pending', 'partial', or 'paid'.")]
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
