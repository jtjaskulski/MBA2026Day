using SolutionOrders.Clients.Domain.Entities;
using SolutionOrders.Workers.Domain.Entities;

namespace SolutionOrders.Orders.Domain.Entities;

/// <summary>
/// Sales order aggregate root (Orders bounded context).
/// </summary>
public class Order
{
    public int IdOrder { get; set; }
    public DateTime? DataOrder { get; set; }
    public int? IdClient { get; set; }
    public int? IdWorker { get; set; }
    public string? Notes { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public virtual Client? Client { get; set; }
    public virtual Worker? Worker { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
