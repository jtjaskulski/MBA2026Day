using MediatR;
using SolutionOrders.UnitOfMeasurements.Application.Messages.DTOs;
using SolutionOrders.UnitOfMeasurements.Application.Messages.Queries;
using SolutionOrders.UnitOfMeasurements.Application.Abstractions;

namespace SolutionOrders.UnitOfMeasurements.Application.Handlers.Queries;

/// <summary>Handles <see cref="GetAllUnitOfMeasurementsQuery"/> via MongoDB projections.</summary>
public sealed class GetAllUnitOfMeasurementsHandler(IUnitOfMeasurementReadRepository readRepository)
    : IRequestHandler<GetAllUnitOfMeasurementsQuery, IEnumerable<UnitOfMeasurementDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<UnitOfMeasurementDto>> Handle(GetAllUnitOfMeasurementsQuery request,
        CancellationToken cancellationToken)
        => await readRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
}
