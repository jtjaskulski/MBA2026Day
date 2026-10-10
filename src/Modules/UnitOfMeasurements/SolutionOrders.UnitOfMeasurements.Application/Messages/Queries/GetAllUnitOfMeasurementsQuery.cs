using MediatR;
using SolutionOrders.UnitOfMeasurements.Application.Messages.DTOs;

namespace SolutionOrders.UnitOfMeasurements.Application.Messages.Queries
{
    public class GetAllUnitOfMeasurementsQuery : IRequest<IEnumerable<UnitOfMeasurementDto>>
    {
    }
}

