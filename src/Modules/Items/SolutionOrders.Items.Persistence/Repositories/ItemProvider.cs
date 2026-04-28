using Microsoft.EntityFrameworkCore;
using SolutionOrders.Items.Application.Abstractions;
using SolutionOrders.Items.Domain.Entities;

namespace SolutionOrders.Items.Persistence.Repositories;

/// <inheritdoc cref="IItemProvider" />
public sealed class ItemProvider(DbContext context) : IItemProvider
{
    /// <inheritdoc />
    public async Task<IEnumerable<Item>> GetAllItemsAsync(bool asNoTracking = true,
        CancellationToken cancellationToken = default)
    {
        var query = context.Set<Item>()
            .Include(i => i.Category)
            .Include(i => i.UnitOfMeasurement)
            .Where(i => i.IsActive);

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.OrderBy(item => item.Name).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Item> GetItemByIdAsync(int id, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = context.Set<Item>()
            .Include(i => i.Category)
            .Include(i => i.UnitOfMeasurement)
            .Where(i => i.IsActive);

        if (asNoTracking)
            query = query.AsNoTracking();

        var item = await query.FirstOrDefaultAsync(i => i.IdItem == id, cancellationToken).ConfigureAwait(false);
        return item ?? throw new KeyNotFoundException($"Produkt o ID {id} nie istnieje");
    }
}
