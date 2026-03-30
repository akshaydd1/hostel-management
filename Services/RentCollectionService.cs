using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;
using HostelManagementApi.Models;
using HostelManagementApi.Repositories.Interfaces;
using HostelManagementApi.Services.Interfaces;

namespace HostelManagementApi.Services
{
    public class RentCollectionService : IRentCollectionService
    {
        private readonly IRentCollectionRepository _rentCollectionRepository;

        public RentCollectionService(IRentCollectionRepository rentCollectionRepository)
        {
            _rentCollectionRepository = rentCollectionRepository;
        }

        public async Task<IEnumerable<RentCollectionResponse>> GetAllRentCollectionsAsync()
        {
            var rentCollections = await _rentCollectionRepository.GetAllAsync();
            return rentCollections.Select(MapToResponse);
        }

        public async Task<RentCollectionResponse?> GetRentCollectionByIdAsync(int id)
        {
            var rentCollection = await _rentCollectionRepository.GetByIdAsync(id);
            return rentCollection == null ? null : MapToResponse(rentCollection);
        }

        public async Task<IEnumerable<RentCollectionResponse>> GetRentCollectionsByStudentIdAsync(int studentId)
        {
            var rentCollections = await _rentCollectionRepository.GetByStudentIdAsync(studentId);
            return rentCollections.Select(MapToResponse);
        }

        public async Task<IEnumerable<RentCollectionResponse>> GetRentCollectionsByRoomIdAsync(int roomId)
        {
            var rentCollections = await _rentCollectionRepository.GetByRoomIdAsync(roomId);
            return rentCollections.Select(MapToResponse);
        }

        public async Task<RentCollectionResponse> CreateRentCollectionAsync(CreateRentCollectionRequest request)
        {
            var status = "pending";
            if (request.PaidRent >= request.TotalRent)
                status = "paid";
            else if (request.PaidRent > 0)
                status = "partial";

            var rentCollection = new RentCollection
            {
                StudentId = request.StudentId,
                RoomId = request.RoomId,
                TotalRent = request.TotalRent,
                PaidRent = request.PaidRent,
                Status = status,
                BalanceRent = request.TotalRent - request.PaidRent,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var created = await _rentCollectionRepository.CreateAsync(rentCollection);
            return MapToResponse(created);
        }

        public async Task<RentCollectionResponse?> UpdateRentCollectionAsync(int id, UpdateRentCollectionRequest request)
        {
            var rentCollection = await _rentCollectionRepository.GetByIdAsync(id);
            if (rentCollection == null) return null;

            if (request.StudentId.HasValue)
                rentCollection.StudentId = request.StudentId.Value;
            if (request.RoomId.HasValue)
                rentCollection.RoomId = request.RoomId.Value;
            if (request.TotalRent.HasValue)
                rentCollection.TotalRent = request.TotalRent.Value;
            if (request.PaidRent.HasValue)
                rentCollection.PaidRent = request.PaidRent.Value;

            // Auto-calculate status based on paid vs total
            if (request.Status != null)
            {
                rentCollection.Status = request.Status;
            }
            else if (request.TotalRent.HasValue || request.PaidRent.HasValue)
            {
                if (rentCollection.PaidRent >= rentCollection.TotalRent)
                    rentCollection.Status = "paid";
                else if (rentCollection.PaidRent > 0)
                    rentCollection.Status = "partial";
                else
                    rentCollection.Status = "pending";
            }

            rentCollection.BalanceRent = rentCollection.TotalRent - rentCollection.PaidRent;
            rentCollection.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _rentCollectionRepository.UpdateAsync(rentCollection);
            return MapToResponse(updated);
        }

        public async Task<bool> DeleteRentCollectionAsync(int id)
        {
            var rentCollection = await _rentCollectionRepository.GetByIdAsync(id);
            if (rentCollection == null) return false;

            await _rentCollectionRepository.DeleteAsync(rentCollection);
            return true;
        }

        private static RentCollectionResponse MapToResponse(RentCollection rentCollection)
        {
            return new RentCollectionResponse
            {
                Id = rentCollection.Id,
                StudentId = rentCollection.StudentId,
                RoomId = rentCollection.RoomId,
                TotalRent = rentCollection.TotalRent,
                PaidRent = rentCollection.PaidRent,
                Status = rentCollection.Status ?? "pending",
                BalanceRent = rentCollection.BalanceRent,
                CreatedAt = rentCollection.CreatedAt,
                UpdatedAt = rentCollection.UpdatedAt
            };
        }
    }
}
