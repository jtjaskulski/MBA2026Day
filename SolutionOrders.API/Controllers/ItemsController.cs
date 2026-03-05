using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolutionOrders.API.Features.Items.Messages.Commands;
using SolutionOrders.API.Features.Items.Messages.DTOs;
using SolutionOrders.API.Features.Items.Messages.Queries;

namespace SolutionOrders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllItems()
        {
            // Creation of Query
            var query = new GetAllItemsQuery();
            
            // Sending to MediatR
            return Ok(await mediator.Send(query));
        }

        /// <summary>
        /// Getting item by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetItemByIdQuery(id);
            var result = await mediator.Send(query);
            
            if (result == null)
            {
                return NotFound(new { message = $"Item with ID {id} doesn't exist." });
            }
            
            return Ok(result);
        }

        /// <summary>
        /// Creates new item
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateItemCommand command)
        {
            var itemId = await mediator.Send(command);
            
            // HTTP 201 Created Location header
            return CreatedAtAction(nameof(GetById), new { id = itemId },
                new { id = itemId, message = "Item is created." }
            );
        }

        /// <summary>
        /// Updates item
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateItemCommand command)
        {
            if (id != command.IdItem)
            {
                return BadRequest(new { message = "ID w URL różni się od ID w body" });
            }
            
            try
            {
                await mediator.Send(command);
                return NoContent(); // HTTP 204
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        ///
        /// Deletes the item (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteItemCommand(id);
            
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