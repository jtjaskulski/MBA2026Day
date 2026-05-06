using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionOrders.Clients.Domain.Entities;

namespace SolutionOrders.Clients.Persistence.EntityConfigurations;

/// <summary>
/// EF mapping for <see cref="Client"/>.
/// </summary>
public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Client> entity)
    {
        entity.ToTable("Clients");
        entity.HasKey(e => e.IdClient);
        entity.Property(e => e.IsActive).IsRequired();
    }
}
