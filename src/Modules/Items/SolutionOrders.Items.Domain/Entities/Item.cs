using SolutionOrders.Categories.Domain.Entities;
using SolutionOrders.UnitOfMeasurements.Domain.Entities;

namespace SolutionOrders.Items.Domain.Entities;

/// <summary>
/// Catalog item (Items bounded context); references Category and UnitOfMeasurement by FK + navigation.
/// </summary>
public class Item
{
    public int IdItem { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int IdCategory { get; set; }
    public decimal? Price { get; set; }
    public decimal? Quantity { get; set; }
    public string? FotoUrl { get; set; }
    public int? IdUnitOfMeasurement { get; set; }
    public string? Code { get; set; }
    public bool IsActive { get; set; }

    public virtual Category Category { get; set; } = null!;
    public virtual UnitOfMeasurement? UnitOfMeasurement { get; set; }
}
