using Microsoft.EntityFrameworkCore;
using HostelManagementApi.Data;
using HostelManagementApi.Models;
using HostelManagementApi.Repositories.Interfaces;

namespace HostelManagementApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HostelDbContext _db;

        public UserRepository(HostelDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.Users.AnyAsync(u => u.Id == id);
        }
    }
}
