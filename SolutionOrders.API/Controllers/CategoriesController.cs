using MediatR;
using Microsoft.AspNetCore.Mvc; 
using SolutionOrders.Categories.Application.Messages.DTOs;
using SolutionOrders.Categories.Application.Messages.Queries;

namespace SolutionOrders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            // Creation of Query
            var query = new GetAllCategoriesQuery();
            
            // Sending to MediatR
            return Ok(await mediator.Send(query));
        }
    }
}
