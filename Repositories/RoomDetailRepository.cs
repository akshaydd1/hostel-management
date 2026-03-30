using Microsoft.EntityFrameworkCore;
using HostelManagementApi.Data;
using HostelManagementApi.Models;
using HostelManagementApi.Repositories.Interfaces;

namespace HostelManagementApi.Repositories
{
    public class RoomDetailRepository : IRoomDetailRepository
    {
        private readonly HostelDbContext _db;

        public RoomDetailRepository(HostelDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RoomDetail>> GetAllAsync()
        {
            return await _db.RoomDetails.AsNoTracking().ToListAsync();
        }

        public async Task<RoomDetail?> GetByIdAsync(int id)
        {
            return await _db.RoomDetails.FindAsync(id);
        }

        public async Task<RoomDetail?> GetByRoomNoAsync(string roomNo)
        {
            return await _db.RoomDetails.FirstOrDefaultAsync(r => r.RoomNo == roomNo);
        }

        public async Task<RoomDetail> CreateAsync(RoomDetail roomDetail)
        {
            _db.RoomDetails.Add(roomDetail);
            await _db.SaveChangesAsync();
            return roomDetail;
        }

        public async Task<RoomDetail> UpdateAsync(RoomDetail roomDetail)
        {
            _db.RoomDetails.Update(roomDetail);
            await _db.SaveChangesAsync();
            return roomDetail;
        }

        public async Task DeleteAsync(RoomDetail roomDetail)
        {
            _db.RoomDetails.Remove(roomDetail);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.RoomDetails.AnyAsync(r => r.Id == id);
        }
    }
}
