using MediatR;

namespace SolutionOrders.Workers.Application.Messages.Commands
{
    public class DeleteWorkerCommand(int idWorker) : IRequest<Unit>
    {
        public int IdWorker { get; set; } = idWorker;
    }
}


