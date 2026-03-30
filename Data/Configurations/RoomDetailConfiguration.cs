using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HostelManagementApi.Models;

namespace HostelManagementApi.Data.Configurations
{
    public class RoomDetailConfiguration : IEntityTypeConfiguration<RoomDetail>
    {
        public void Configure(EntityTypeBuilder<RoomDetail> builder)
        {
            builder.ToTable("room_detail", "public");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.RoomNo)
                .HasColumnName("room_no")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.FloorNo)
                .HasColumnName("floor_no")
                .IsRequired();

            builder.Property(e => e.RoomType)
                .HasColumnName("room_type")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("timestamp without time zone");
        }
    }
}
