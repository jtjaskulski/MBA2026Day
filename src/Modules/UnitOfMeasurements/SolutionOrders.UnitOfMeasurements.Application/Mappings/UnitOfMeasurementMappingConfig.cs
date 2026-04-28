using Mapster;
using SolutionOrders.UnitOfMeasurements.Application.Messages.DTOs;
using SolutionOrders.UnitOfMeasurements.Domain.Entities;

namespace SolutionOrders.UnitOfMeasurements.Application.Mappings
{
    public class UnitOfMeasurementMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UnitOfMeasurement, UnitOfMeasurementDto>();
        }
    }
}

