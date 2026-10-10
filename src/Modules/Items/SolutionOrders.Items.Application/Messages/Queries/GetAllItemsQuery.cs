using MediatR;
using SolutionOrders.Items.Application.Messages.DTOs;

namespace SolutionOrders.Items.Application.Messages.Queries
{
    public class GetAllItemsQuery : IRequest<IEnumerable<ItemDto>>
    {
    }
}

