using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolutionOrders.Categories.Domain.Entities;

namespace SolutionOrders.Categories.Persistence.EntityConfigurations;

/// <summary>
/// EF mapping for <see cref="Category"/> (SQL Server).
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.ToTable("Categories");
        entity.HasKey(e => e.IdCategory);
        entity.Property(e => e.IsActive).IsRequired();
    }
}
