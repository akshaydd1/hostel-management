using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HostelManagementApi.DTOs.Requests
{
    public class CreateRoomDetailRequest
    {
        [Required(ErrorMessage = "Room number is required.")]
        [MaxLength(50, ErrorMessage = "Room number must not exceed 50 characters.")]
        [JsonPropertyName("room_no")]
        public string RoomNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Floor number is required.")]
        [JsonPropertyName("floor_no")]
        public int FloorNo { get; set; }

        [Required(ErrorMessage = "Room type is required.")]
        [MaxLength(50, ErrorMessage = "Room type must not exceed 50 characters.")]
        [JsonPropertyName("room_type")]
        public string RoomType { get; set; } = string.Empty;
    }
}
