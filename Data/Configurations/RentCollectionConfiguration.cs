using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HostelManagementApi.Models;

namespace HostelManagementApi.Data.Configurations
{
    public class RentCollectionConfiguration : IEntityTypeConfiguration<RentCollection>
    {
        public void Configure(EntityTypeBuilder<RentCollection> builder)
        {
            builder.ToTable("rent_collection", "public");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.StudentId)
                .HasColumnName("student_id")
                .IsRequired();

            builder.Property(e => e.RoomId)
                .HasColumnName("room_id")
                .IsRequired();

            builder.Property(e => e.TotalRent)
                .HasColumnName("total_rent")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.PaidRent)
                .HasColumnName("paid_rent")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .IsRequired();

            builder.Property(e => e.BalanceRent)
                .HasColumnName("balance_rent")
                .HasColumnType("decimal(10,2)")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            // Relationships
            builder.HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Room)
                .WithMany()
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
