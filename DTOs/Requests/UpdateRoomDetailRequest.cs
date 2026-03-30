using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HostelManagementApi.DTOs.Requests
{
    public class UpdateRoomDetailRequest
    {
        [MaxLength(50, ErrorMessage = "Room number must not exceed 50 characters.")]
        [JsonPropertyName("room_no")]
        public string? RoomNo { get; set; }

        [JsonPropertyName("floor_no")]
        public int? FloorNo { get; set; }

        [MaxLength(50, ErrorMessage = "Room type must not exceed 50 characters.")]
        [JsonPropertyName("room_type")]
        public string? RoomType { get; set; }
    }
}
