namespace SolutionOrders.Categories.Domain.Entities;

/// <summary>
/// Product category aggregate root (Categories bounded context).
/// </summary>
public class Category
{
    public int IdCategory { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
