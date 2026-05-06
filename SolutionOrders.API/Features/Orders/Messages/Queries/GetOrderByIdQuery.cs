using MediatR;
using SolutionOrders.API.Features.Orders.Messages.DTOs;

namespace SolutionOrders.API.Features.Orders.Messages.Queries
{
    public class GetOrderByIdQuery(int id) : IRequest<OrderDto?>
    {
        public int Id { get; set; } = id;
    }
}
