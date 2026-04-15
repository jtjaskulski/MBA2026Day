namespace SolutionOrders.API.Features.Categories.Messages.DTOs
{
    public class CategoryDto
    {
        public int IdCategory { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}