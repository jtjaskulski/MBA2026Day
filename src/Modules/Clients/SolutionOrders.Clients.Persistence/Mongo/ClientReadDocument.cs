using MongoDB.Bson.Serialization.Attributes;
using SolutionOrders.Clients.Application.Messages.DTOs;

namespace SolutionOrders.Clients.Persistence.Mongo;

/// <summary>BSON projection for client reads (synced from SQL).</summary>
public sealed class ClientReadDocument
{
    /// <summary>MongoDB document id (same as SQL PK).</summary>
    [BsonId]
    public int IdClient { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    /// <summary>Maps to API DTO.</summary>
    public ClientDto ToDto() =>
        new()
        {
            IdClient = IdClient,
            Name = Name,
            Address = Address,
            PhoneNumber = PhoneNumber,
            IsActive = IsActive
        };
}
