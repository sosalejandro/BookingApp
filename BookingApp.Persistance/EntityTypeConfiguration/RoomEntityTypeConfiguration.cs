using BookingApp.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingApp.Persistance.EntityTypeConfiguration;
internal sealed class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired();            

        builder.Property(r => r.CreatedDate)
            .IsRequired()
            .HasColumnName("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(r => r.ModifiedDate)
            .ValueGeneratedOnUpdate();
    }
}