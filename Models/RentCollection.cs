namespace HostelManagementApi.Models
{
    public class RentCollection
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RoomId { get; set; }
        public decimal TotalRent { get; set; }
        public decimal PaidRent { get; set; }
        public string Status { get; set; } = "pending";
        public decimal BalanceRent { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        // Navigation properties
        public User? Student { get; set; }
        public RoomDetail? Room { get; set; }
    }
}
