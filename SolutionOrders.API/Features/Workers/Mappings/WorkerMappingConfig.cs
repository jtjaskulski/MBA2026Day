using Mapster;
using SolutionOrders.API.Features.Workers.Messages.Commands;
using SolutionOrders.API.Features.Workers.Messages.DTOs;
using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Workers.Mappings
{
    public class WorkerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Worker, WorkerDto>();

            config.NewConfig<CreateWorkerCommand, Worker>()
                .Map(dest => dest.IsActive, _ => true)
                .Ignore(dest => dest.IdWorker)
                .Ignore(dest => dest.Orders);

            config.NewConfig<UpdateWorkerCommand, Worker>()
                .Ignore(dest => dest.IdWorker)
                .Ignore(dest => dest.Orders);
        }
    }
}
