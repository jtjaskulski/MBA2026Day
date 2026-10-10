using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionOrders.Items.Domain.Entities;

namespace SolutionOrders.Items.Persistence.EntityConfigurations;

/// <summary>
/// EF mapping for catalog <see cref="Item"/>.
/// </summary>
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Item> entity)
    {
        entity.ToTable("Items");
        entity.HasKey(e => e.IdItem);
        entity.Property(e => e.IdCategory).IsRequired();
        entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.IsActive).IsRequired();

        entity.HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.IdCategory)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.UnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.IdUnitOfMeasurement)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
