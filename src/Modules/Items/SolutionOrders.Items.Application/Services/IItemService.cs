using SolutionOrders.Items.Domain.Entities;

namespace SolutionOrders.Items.Application.Services;

public interface IItemService
{
    Task CreateItem(Item item, CancellationToken cancellationToken);
}

