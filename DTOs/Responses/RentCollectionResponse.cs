using System.Text.Json.Serialization;

namespace HostelManagementApi.DTOs.Responses
{
    public class RentCollectionResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("student_id")]
        public int StudentId { get; set; }

        [JsonPropertyName("room_id")]
        public int RoomId { get; set; }

        [JsonPropertyName("total_rent")]
        public decimal TotalRent { get; set; }

        [JsonPropertyName("paid_rent")]
        public decimal PaidRent { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("balance_rent")]
        public decimal BalanceRent { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
