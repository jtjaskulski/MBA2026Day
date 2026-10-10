using Mapster;
using SolutionOrders.Workers.Application.Messages.Commands;
using SolutionOrders.Workers.Application.Messages.DTOs;
using SolutionOrders.Workers.Domain.Entities;

namespace SolutionOrders.Workers.Application.Mappings
{
    public class WorkerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Worker, WorkerDto>();

            config.NewConfig<CreateWorkerCommand, Worker>()
                .Map(dest => dest.IsActive, _ => true)
                .Ignore(dest => dest.IdWorker);

            config.NewConfig<UpdateWorkerCommand, Worker>()
                .Ignore(dest => dest.IdWorker);
        }
    }
}


