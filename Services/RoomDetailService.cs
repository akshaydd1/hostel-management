using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;
using HostelManagementApi.Models;
using HostelManagementApi.Repositories.Interfaces;
using HostelManagementApi.Services.Interfaces;

namespace HostelManagementApi.Services
{
    public class RoomDetailService : IRoomDetailService
    {
        private readonly IRoomDetailRepository _roomDetailRepository;

        public RoomDetailService(IRoomDetailRepository roomDetailRepository)
        {
            _roomDetailRepository = roomDetailRepository;
        }

        public async Task<IEnumerable<RoomDetailResponse>> GetAllRoomDetailsAsync()
        {
            var roomDetails = await _roomDetailRepository.GetAllAsync();
            return roomDetails.Select(MapToResponse);
        }

        public async Task<RoomDetailResponse?> GetRoomDetailByIdAsync(int id)
        {
            var roomDetail = await _roomDetailRepository.GetByIdAsync(id);
            return roomDetail == null ? null : MapToResponse(roomDetail);
        }

        public async Task<RoomDetailResponse> CreateRoomDetailAsync(CreateRoomDetailRequest request)
        {
            var roomDetail = new RoomDetail
            {
                RoomNo = request.RoomNo.Trim(),
                FloorNo = request.FloorNo,
                RoomType = request.RoomType.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            var created = await _roomDetailRepository.CreateAsync(roomDetail);
            return MapToResponse(created);
        }

        public async Task<RoomDetailResponse?> UpdateRoomDetailAsync(int id, UpdateRoomDetailRequest request)
        {
            var roomDetail = await _roomDetailRepository.GetByIdAsync(id);
            if (roomDetail == null) return null;

            if (!string.IsNullOrWhiteSpace(request.RoomNo))
                roomDetail.RoomNo = request.RoomNo.Trim();
            if (request.FloorNo.HasValue)
                roomDetail.FloorNo = request.FloorNo.Value;
            if (!string.IsNullOrWhiteSpace(request.RoomType))
                roomDetail.RoomType = request.RoomType.Trim();

            roomDetail.UpdatedAt = DateTime.UtcNow;

            var updated = await _roomDetailRepository.UpdateAsync(roomDetail);
            return MapToResponse(updated);
        }

        public async Task<bool> DeleteRoomDetailAsync(int id)
        {
            var roomDetail = await _roomDetailRepository.GetByIdAsync(id);
            if (roomDetail == null) return false;

            await _roomDetailRepository.DeleteAsync(roomDetail);
            return true;
        }

        private static RoomDetailResponse MapToResponse(RoomDetail roomDetail)
        {
            return new RoomDetailResponse
            {
                Id = roomDetail.Id,
                RoomNo = roomDetail.RoomNo ?? string.Empty,
                FloorNo = roomDetail.FloorNo,
                RoomType = roomDetail.RoomType ?? string.Empty,
                CreatedAt = roomDetail.CreatedAt,
                UpdatedAt = roomDetail.UpdatedAt
            };
        }
    }
}
