using MediatR;
using SolutionOrders.Orders.Application.Messages.DTOs;

namespace SolutionOrders.Orders.Application.Messages.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int? IdClient { get; set; }
        public int? IdWorker { get; set; }
        public string? Notes { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; } = [];
    }
}

