using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;

namespace HostelManagementApi.Services.Interfaces
{
    public interface IRoomDetailService
    {
        Task<IEnumerable<RoomDetailResponse>> GetAllRoomDetailsAsync();
        Task<RoomDetailResponse?> GetRoomDetailByIdAsync(int id);
        Task<RoomDetailResponse> CreateRoomDetailAsync(CreateRoomDetailRequest request);
        Task<RoomDetailResponse?> UpdateRoomDetailAsync(int id, UpdateRoomDetailRequest request);
        Task<bool> DeleteRoomDetailAsync(int id);
    }
}
