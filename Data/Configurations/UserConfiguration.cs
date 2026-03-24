using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HostelManagementApi.Models;

namespace HostelManagementApi.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", "public");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(e => e.City)
                .HasColumnName("city")
                .HasMaxLength(100);

            builder.Property(e => e.State)
                .HasColumnName("state")
                .HasMaxLength(100);

            builder.Property(e => e.DocType)
                .HasColumnName("doc_type")
                .HasMaxLength(50);

            builder.Property(e => e.DocNumber)
                .HasColumnName("doc_number")
                .HasMaxLength(100);

            builder.Property(e => e.MobileNo)
                .HasColumnName("mobile_no")
                .HasMaxLength(15);

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at");
        }
    }
}
