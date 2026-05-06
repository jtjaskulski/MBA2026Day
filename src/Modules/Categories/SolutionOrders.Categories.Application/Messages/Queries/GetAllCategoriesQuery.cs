using MediatR;
using SolutionOrders.Categories.Application.Messages.DTOs;

namespace SolutionOrders.Categories.Application.Messages.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}
