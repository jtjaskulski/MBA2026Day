namespace SolutionOrders.UnitOfMeasurements.Domain.Entities;

/// <summary>
/// Unit of measurement entity (UnitOfMeasurements bounded context).
/// </summary>
public class UnitOfMeasurement
{
    public int IdUnitOfMeasurement { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
