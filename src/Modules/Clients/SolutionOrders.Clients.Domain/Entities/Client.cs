namespace SolutionOrders.Clients.Domain.Entities;

/// <summary>
/// Customer/client aggregate root (Clients bounded context).
/// Inverse navigation to orders is omitted to avoid circular domain references; EF maps FK from Order.
/// </summary>
public class Client
{
    public int IdClient { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}
