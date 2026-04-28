using MongoDB.Bson.Serialization.Attributes;
using SolutionOrders.Workers.Application.Messages.DTOs;

namespace SolutionOrders.Workers.Persistence.Mongo;

/// <summary>BSON projection for worker reads (synced from SQL).</summary>
public sealed class WorkerReadDocument
{
    [BsonId]
    public int IdWorker { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Login { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public WorkerDto ToDto() =>
        new()
        {
            IdWorker = IdWorker,
            FirstName = FirstName,
            LastName = LastName,
            Login = Login,
            IsActive = IsActive
        };
}
