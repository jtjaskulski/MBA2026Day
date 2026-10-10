using MediatR;

namespace SolutionOrders.Items.Application.Messages.Commands
{
    public class DeleteItemCommand(int idItem) : IRequest<Unit>
    {
        public int IdItem { get; set; } = idItem;
    }
}

