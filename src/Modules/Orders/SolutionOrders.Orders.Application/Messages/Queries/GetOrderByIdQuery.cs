using MediatR;
using SolutionOrders.Orders.Application.Messages.DTOs;

namespace SolutionOrders.Orders.Application.Messages.Queries
{
    public class GetOrderByIdQuery(int id) : IRequest<OrderDto?>
    {
        public int Id { get; set; } = id;
    }
}


