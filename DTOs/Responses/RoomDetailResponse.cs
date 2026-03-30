using System.Text.Json.Serialization;

namespace HostelManagementApi.DTOs.Responses
{
    public class RoomDetailResponse
    {
        public int Id { get; set; }

        [JsonPropertyName("room_no")]
        public string RoomNo { get; set; } = string.Empty;

        [JsonPropertyName("floor_no")]
        public int FloorNo { get; set; }

        [JsonPropertyName("room_type")]
        public string RoomType { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
