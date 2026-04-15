using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolutionOrders.API.Features.Workers.Messages.Commands;
using SolutionOrders.API.Features.Workers.Messages.DTOs;
using SolutionOrders.API.Features.Workers.Messages.Queries;

namespace SolutionOrders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkerDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWorkers()
        {
            var query = new GetAllWorkersQuery();
            return Ok(await mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetWorkerByIdQuery(id);
            var result = await mediator.Send(query);

            if (result == null)
                return NotFound(new { message = $"Worker with ID {id} doesn't exist." });

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateWorkerCommand command)
        {
            var workerId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = workerId },
                new { id = workerId, message = "Worker created." });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWorkerCommand command)
        {
            if (id != command.IdWorker)
                return BadRequest(new { message = "ID in URL differs from ID in body" });

            try
            {
                await mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteWorkerCommand(id);
            try
            {
                await mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}