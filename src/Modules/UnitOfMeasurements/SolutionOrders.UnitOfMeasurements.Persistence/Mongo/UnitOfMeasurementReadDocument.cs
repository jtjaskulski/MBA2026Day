using MongoDB.Bson.Serialization.Attributes;
using SolutionOrders.UnitOfMeasurements.Application.Messages.DTOs;

namespace SolutionOrders.UnitOfMeasurements.Persistence.Mongo;

/// <summary>BSON projection for unit-of-measurement reads (synced from SQL).</summary>
public sealed class UnitOfMeasurementReadDocument
{
    [BsonId]
    public int IdUnitOfMeasurement { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public UnitOfMeasurementDto ToDto() =>
        new()
        {
            IdUnitOfMeasurement = IdUnitOfMeasurement,
            Name = Name,
            Description = Description,
            IsActive = IsActive
        };
}
