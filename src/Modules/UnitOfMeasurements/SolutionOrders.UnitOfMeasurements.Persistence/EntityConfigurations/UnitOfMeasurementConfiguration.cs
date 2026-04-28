using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionOrders.UnitOfMeasurements.Domain.Entities;

namespace SolutionOrders.UnitOfMeasurements.Persistence.EntityConfigurations;

/// <summary>
/// EF mapping for <see cref="UnitOfMeasurement"/>.
/// </summary>
public class UnitOfMeasurementConfiguration : IEntityTypeConfiguration<UnitOfMeasurement>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UnitOfMeasurement> entity)
    {
        entity.ToTable("UnitOfMeasurements");
        entity.HasKey(e => e.IdUnitOfMeasurement);
        entity.Property(e => e.IsActive).IsRequired();
    }
}
