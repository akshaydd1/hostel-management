using Microsoft.EntityFrameworkCore;
using HostelManagementApi.Data;
using HostelManagementApi.Models;
using HostelManagementApi.Repositories.Interfaces;

namespace HostelManagementApi.Repositories
{
    public class RentCollectionRepository : IRentCollectionRepository
    {
        private readonly HostelDbContext _db;

        public RentCollectionRepository(HostelDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RentCollection>> GetAllAsync()
        {
            return await _db.RentCollections.AsNoTracking().ToListAsync();
        }

        public async Task<RentCollection?> GetByIdAsync(int id)
        {
            return await _db.RentCollections.FindAsync(id);
        }

        public async Task<IEnumerable<RentCollection>> GetByStudentIdAsync(int studentId)
        {
            return await _db.RentCollections
                .AsNoTracking()
                .Where(r => r.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RentCollection>> GetByRoomIdAsync(int roomId)
        {
            return await _db.RentCollections
                .AsNoTracking()
                .Where(r => r.RoomId == roomId)
                .ToListAsync();
        }

        public async Task<RentCollection> CreateAsync(RentCollection rentCollection)
        {
            _db.RentCollections.Add(rentCollection);
            await _db.SaveChangesAsync();
            return rentCollection;
        }

        public async Task<RentCollection> UpdateAsync(RentCollection rentCollection)
        {
            _db.RentCollections.Update(rentCollection);
            await _db.SaveChangesAsync();
            return rentCollection;
        }

        public async Task DeleteAsync(RentCollection rentCollection)
        {
            _db.RentCollections.Remove(rentCollection);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.RentCollections.AnyAsync(r => r.Id == id);
        }
    }
}
