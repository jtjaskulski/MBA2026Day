using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionOrders.Orders.Domain.Entities;

namespace SolutionOrders.Orders.Persistence.EntityConfigurations;

/// <summary>
/// EF mapping for <see cref="Order"/>.
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.ToTable("Orders");
        entity.HasKey(e => e.IdOrder);

        entity.HasOne(e => e.Client)
            .WithMany()
            .HasForeignKey(e => e.IdClient)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Worker)
            .WithMany()
            .HasForeignKey(e => e.IdWorker)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
