using Microsoft.EntityFrameworkCore;
using SolutionOrders.Items.Application.Services;
using SolutionOrders.Items.Domain.Entities;

namespace SolutionOrders.Items.Persistence.Services;

/// <inheritdoc cref="IItemService" />
public sealed class ItemService(DbContext context) : IItemService
{
    /// <inheritdoc />
    public async Task CreateItem(Item item, CancellationToken cancellationToken)
    {
        context.Set<Item>().Add(item);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
