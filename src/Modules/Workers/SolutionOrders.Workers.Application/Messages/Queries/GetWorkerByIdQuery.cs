using MediatR;
using SolutionOrders.Workers.Application.Messages.DTOs;

namespace SolutionOrders.Workers.Application.Messages.Queries
{
    public class GetWorkerByIdQuery(int id) : IRequest<WorkerDto?>
    {
        public int Id { get; set; } = id;
    }
}


