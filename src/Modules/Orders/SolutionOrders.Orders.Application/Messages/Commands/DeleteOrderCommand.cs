using MediatR;

namespace SolutionOrders.Orders.Application.Messages.Commands
{
    public class DeleteOrderCommand(int idOrder) : IRequest<Unit>
    {
        public int IdOrder { get; set; } = idOrder;
    }
}

