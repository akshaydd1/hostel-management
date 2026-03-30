using HostelManagementApi.Models;

namespace HostelManagementApi.Repositories.Interfaces
{
    public interface IRoomDetailRepository
    {
        Task<IEnumerable<RoomDetail>> GetAllAsync();
        Task<RoomDetail?> GetByIdAsync(int id);
        Task<RoomDetail?> GetByRoomNoAsync(string roomNo);
        Task<RoomDetail> CreateAsync(RoomDetail roomDetail);
        Task<RoomDetail> UpdateAsync(RoomDetail roomDetail);
        Task DeleteAsync(RoomDetail roomDetail);
        Task<bool> ExistsAsync(int id);
    }
}
