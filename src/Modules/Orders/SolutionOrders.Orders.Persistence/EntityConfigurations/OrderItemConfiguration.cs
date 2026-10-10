using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionOrders.Orders.Domain.Entities;

namespace SolutionOrders.Orders.Persistence.EntityConfigurations;

/// <summary>
/// EF mapping for <see cref="OrderItem"/>.
/// </summary>
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<OrderItem> entity)
    {
        entity.ToTable("OrderItems");
        entity.HasKey(e => e.IdOrderItem);
        entity.Property(e => e.IdOrder).IsRequired();
        entity.Property(e => e.IdItem).IsRequired();
        entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.IsActive).IsRequired();

        entity.HasOne(e => e.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(e => e.IdOrder)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.IdItem)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
