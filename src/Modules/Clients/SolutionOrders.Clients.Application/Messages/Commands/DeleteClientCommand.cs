using MediatR;

namespace SolutionOrders.Clients.Application.Messages.Commands
{
    public class DeleteClientCommand(int idClient) : IRequest<Unit>
    {
        public int IdClient { get; set; } = idClient;
    }
}



