using Microsoft.EntityFrameworkCore;
using HostelManagementApi.Data.Configurations;
using HostelManagementApi.Models;

namespace HostelManagementApi.Data
{
    public class HostelDbContext : DbContext
    {
        public HostelDbContext(DbContextOptions<HostelDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoomDetailConfiguration());
        }
    }
}
