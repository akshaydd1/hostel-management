namespace HostelManagementApi.Models
{
    public class RoomDetail
    {
        public int Id { get; set; }
        public string? RoomNo { get; set; }
        public int FloorNo { get; set; }
        public string? RoomType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
