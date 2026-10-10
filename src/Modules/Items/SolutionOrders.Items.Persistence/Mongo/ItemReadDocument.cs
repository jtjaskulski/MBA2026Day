using MongoDB.Bson.Serialization.Attributes;
using SolutionOrders.Items.Application.Messages.DTOs;

namespace SolutionOrders.Items.Persistence.Mongo;

/// <summary>BSON projection for item/catalog reads (synced from SQL).</summary>
public sealed class ItemReadDocument
{
    [BsonId]
    public int IdItem { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public decimal? Quantity { get; set; }

    public int IdCategory { get; set; }

    public string? CategoryName { get; set; }

    public int? IdUnitOfMeasurement { get; set; }

    public string? UnitName { get; set; }

    public string? Code { get; set; }

    public bool IsActive { get; set; }

    public ItemDto ToDto() =>
        new()
        {
            IdItem = IdItem,
            Name = Name,
            Description = Description,
            Price = Price,
            Quantity = Quantity,
            IdCategory = IdCategory,
            CategoryName = CategoryName,
            IdUnitOfMeasurement = IdUnitOfMeasurement,
            UnitName = UnitName,
            Code = Code,
            IsActive = IsActive
        };
}
