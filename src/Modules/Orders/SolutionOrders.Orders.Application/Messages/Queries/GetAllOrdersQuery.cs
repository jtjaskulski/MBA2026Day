using MediatR;
using SolutionOrders.Orders.Application.Messages.DTOs;

namespace SolutionOrders.Orders.Application.Messages.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }
}


