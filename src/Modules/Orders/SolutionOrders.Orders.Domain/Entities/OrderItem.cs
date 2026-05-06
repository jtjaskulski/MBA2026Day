using SolutionOrders.Items.Domain.Entities;

namespace SolutionOrders.Orders.Domain.Entities;

/// <summary>
/// Line item belonging to an order; references catalog Item by FK + navigation.
/// </summary>
public class OrderItem
{
    public int IdOrderItem { get; set; }
    public int IdOrder { get; set; }
    public int IdItem { get; set; }
    public decimal? Quantity { get; set; }
    public bool IsActive { get; set; }

    public virtual Order Order { get; set; } = null!;
    public virtual Item Item { get; set; } = null!;
}
