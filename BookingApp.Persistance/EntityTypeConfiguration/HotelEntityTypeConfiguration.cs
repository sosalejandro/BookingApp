using BookingApp.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingApp.Persistance.EntityTypeConfiguration;

public class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("Hotels");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.CreatedDate)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(h => h.ModifiedDate)
            .ValueGeneratedOnUpdate();

        builder.Property(h => h.Name)
            .IsRequired();

        builder.Property(h => h.Type)
            .IsRequired();

        builder.Property(h => h.City)
            .IsRequired();

        builder.Property(h => h.Address)
            .IsRequired();

        builder.OwnsMany(h => h.Photos, p =>
        {
            p.ToTable("HotelPhotos");

            p.WithOwner()
            .HasForeignKey("HotelId");

            p.Property(p => p.Link)
            .IsRequired();
        });

        builder.Property(h => h.Description)
            .IsRequired();

        builder.Property(h => h.Rating)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasMany(h => h.Rooms)
            .WithOne()
            .HasForeignKey("HotelId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(h => h.CheapestPrice)
            .IsRequired()
            .HasDefaultValue((long)0);

        builder.Property(h => h.ExpensiestPrice)
            .IsRequired()
            .HasDefaultValue((long)0);

        builder.Property(h => h.Featured)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
