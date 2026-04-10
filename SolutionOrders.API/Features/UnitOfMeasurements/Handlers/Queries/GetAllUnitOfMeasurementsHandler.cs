using Mapster;
using MediatR;
using SolutionOrders.API.Features.UnitOfMeasurements.Messages.DTOs;
using SolutionOrders.API.Features.UnitOfMeasurements.Messages.Queries;
using SolutionOrders.API.Features.UnitOfMeasurements.Providers;

namespace SolutionOrders.API.Features.UnitOfMeasurements.Handlers.Queries
{
    public class GetAllUnitOfMeasurementsHandler(IUnitOfMeasurementProvider unitOfMeasurementProvider)
        : IRequestHandler<GetAllUnitOfMeasurementsQuery, IEnumerable<UnitOfMeasurementDto>>
    {
        public async Task<IEnumerable<UnitOfMeasurementDto>> Handle(GetAllUnitOfMeasurementsQuery request, CancellationToken cancellationToken)
            =>
            (await unitOfMeasurementProvider.GetAllUnitOfMeasurementsAsync(true, cancellationToken))
            .Adapt<IEnumerable<UnitOfMeasurementDto>>();
    }
}