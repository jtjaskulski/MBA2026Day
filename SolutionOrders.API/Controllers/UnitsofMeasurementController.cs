using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolutionOrders.UnitOfMeasurements.Application.Messages.DTOs;
using SolutionOrders.UnitOfMeasurements.Application.Messages.Queries;

namespace SolutionOrders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasurementsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UnitOfMeasurementDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUnitOfMeasurements()
        {
            // Creation of Query
            var query = new GetAllUnitOfMeasurementsQuery();
            
            // Sending to MediatR
            return Ok(await mediator.Send(query));
        }
    }
}
