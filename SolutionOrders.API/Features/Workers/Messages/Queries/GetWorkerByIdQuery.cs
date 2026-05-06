using MediatR;
using SolutionOrders.API.Features.Workers.Messages.DTOs;

namespace SolutionOrders.API.Features.Workers.Messages.Queries
{
    public class GetWorkerByIdQuery(int id) : IRequest<WorkerDto?>
    {
        public int Id { get; set; } = id;
    }
}
