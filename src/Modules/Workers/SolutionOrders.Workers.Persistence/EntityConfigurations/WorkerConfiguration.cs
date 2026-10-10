using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionOrders.Workers.Domain.Entities;

namespace SolutionOrders.Workers.Persistence.EntityConfigurations;

/// <summary>
/// EF mapping for <see cref="Worker"/>.
/// </summary>
public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Worker> entity)
    {
        entity.ToTable("Workers");
        entity.HasKey(e => e.IdWorker);
        entity.Property(e => e.Login).IsRequired();
        entity.Property(e => e.IsActive).IsRequired();
    }
}
