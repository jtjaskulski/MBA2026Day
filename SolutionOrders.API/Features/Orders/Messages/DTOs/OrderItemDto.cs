namespace SolutionOrders.API.Features.Orders.Messages.DTOs
{
    public class OrderItemDto
    {
        public int IdOrderItem { get; set; }
        public int IdItem { get; set; }
        public string? ItemName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool IsActive { get; set; }
    }
}
