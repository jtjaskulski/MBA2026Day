using MongoDB.Bson.Serialization.Attributes;
using SolutionOrders.Categories.Application.Messages.DTOs;

namespace SolutionOrders.Categories.Persistence.Mongo;

/// <summary>
/// BSON document stored in MongoDB for category read APIs (projection from SQL).
/// </summary>
public sealed class CategoryReadDocument
{
    /// <summary>MongoDB document identifier (same as SQL PK).</summary>
    [BsonId]
    public int IdCategory { get; set; }

    /// <inheritdoc cref="CategoryDto.Name"/>
    public string? Name { get; set; }

    /// <inheritdoc cref="CategoryDto.Description"/>
    public string? Description { get; set; }

    /// <inheritdoc cref="CategoryDto.IsActive"/>
    public bool IsActive { get; set; }

    /// <summary>
    /// Maps this document to API DTO.
    /// </summary>
    public CategoryDto ToDto() =>
        new()
        {
            IdCategory = IdCategory,
            Name = Name,
            Description = Description,
            IsActive = IsActive
        };
}
