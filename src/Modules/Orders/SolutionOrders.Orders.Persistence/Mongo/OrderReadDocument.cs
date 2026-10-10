using MongoDB.Bson.Serialization.Attributes;
using SolutionOrders.Orders.Application.Messages.DTOs;

namespace SolutionOrders.Orders.Persistence.Mongo;

/// <summary>Line item embedded in <see cref="OrderReadDocument"/>.</summary>
public sealed class OrderItemReadDocument
{
    public int IdOrderItem { get; set; }

    public int IdItem { get; set; }

    public string? ItemName { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Price { get; set; }

    public bool IsActive { get; set; }

    public OrderItemDto ToDto() =>
        new()
        {
            IdOrderItem = IdOrderItem,
            IdItem = IdItem,
            ItemName = ItemName,
            Quantity = Quantity,
            Price = Price,
            IsActive = IsActive
        };
}

/// <summary>BSON projection for sales orders (synced from SQL).</summary>
public sealed class OrderReadDocument
{
    [BsonId]
    public int IdOrder { get; set; }

    public DateTime? DataOrder { get; set; }

    public int? IdClient { get; set; }

    public string? ClientName { get; set; }

    public int? IdWorker { get; set; }

    public string? WorkerName { get; set; }

    public string? Notes { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public List<OrderItemReadDocument> OrderItems { get; set; } = [];

    public OrderDto ToDto() =>
        new()
        {
            IdOrder = IdOrder,
            DataOrder = DataOrder,
            IdClient = IdClient,
            ClientName = ClientName,
            IdWorker = IdWorker,
            WorkerName = WorkerName,
            Notes = Notes,
            DeliveryDate = DeliveryDate,
            OrderItems = OrderItems.ConvertAll(x => x.ToDto())
        };
}
