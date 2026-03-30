using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;

namespace HostelManagementApi.Services.Interfaces
{
    public interface IRentCollectionService
    {
        Task<IEnumerable<RentCollectionResponse>> GetAllRentCollectionsAsync();
        Task<RentCollectionResponse?> GetRentCollectionByIdAsync(int id);
        Task<IEnumerable<RentCollectionResponse>> GetRentCollectionsByStudentIdAsync(int studentId);
        Task<IEnumerable<RentCollectionResponse>> GetRentCollectionsByRoomIdAsync(int roomId);
        Task<RentCollectionResponse> CreateRentCollectionAsync(CreateRentCollectionRequest request);
        Task<RentCollectionResponse?> UpdateRentCollectionAsync(int id, UpdateRentCollectionRequest request);
        Task<bool> DeleteRentCollectionAsync(int id);
    }
}
