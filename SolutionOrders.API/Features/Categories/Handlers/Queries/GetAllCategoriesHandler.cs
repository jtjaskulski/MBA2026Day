using Mapster;
using MediatR;
using SolutionOrders.API.Features.Categories.Messages.DTOs;
using SolutionOrders.API.Features.Categories.Messages.Queries;
using SolutionOrders.API.Features.Categories.Providers;

namespace SolutionOrders.API.Features.Categories.Handlers.Queries
{
    public class GetAllCategoriesHandler(ICategoryProvider categoryProvider)
        : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            =>
            (await categoryProvider.GetAllCategoriesAsync(true, cancellationToken))
            .Adapt<IEnumerable<CategoryDto>>();
    }
}