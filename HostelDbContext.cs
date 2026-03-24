using Microsoft.EntityFrameworkCore;
using HostelManagementApi.Models;

namespace HostelManagementApi
{
    public class HostelDbContext : DbContext
    {
        public HostelDbContext(DbContextOptions<HostelDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "public");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.City).HasColumnName("city");
                entity.Property(e => e.State).HasColumnName("state");
                entity.Property(e => e.DocType).HasColumnName("doc_type");
                entity.Property(e => e.DocNumber).HasColumnName("doc_number");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });
        }
    }
}