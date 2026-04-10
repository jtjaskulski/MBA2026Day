using Mapster;
using SolutionOrders.API.Features.UnitOfMeasurements.Messages.DTOs;
using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.UnitOfMeasurements.Mappings
{
    public class UnitOfMeasurementMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UnitOfMeasurement, UnitOfMeasurementDto>();
        }
    }
}