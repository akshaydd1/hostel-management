using HostelManagementApi.Models;

namespace HostelManagementApi.Repositories.Interfaces
{
    public interface IRentCollectionRepository
    {
        Task<IEnumerable<RentCollection>> GetAllAsync();
        Task<RentCollection?> GetByIdAsync(int id);
        Task<IEnumerable<RentCollection>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<RentCollection>> GetByRoomIdAsync(int roomId);
        Task<RentCollection> CreateAsync(RentCollection rentCollection);
        Task<RentCollection> UpdateAsync(RentCollection rentCollection);
        Task DeleteAsync(RentCollection rentCollection);
        Task<bool> ExistsAsync(int id);
    }
}
