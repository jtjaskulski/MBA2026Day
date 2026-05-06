namespace SolutionOrders.Workers.Domain.Entities;

/// <summary>
/// Worker (sales user) entity (Workers bounded context).
/// </summary>
public class Worker
{
    public int IdWorker { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsActive { get; set; }
    public string Login { get; set; } = string.Empty;
}
