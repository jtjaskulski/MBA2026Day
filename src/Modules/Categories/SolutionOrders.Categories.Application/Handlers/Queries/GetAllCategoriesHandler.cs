using MediatR;
using SolutionOrders.Categories.Application.Abstractions;
using SolutionOrders.Categories.Application.Messages.DTOs;
using SolutionOrders.Categories.Application.Messages.Queries;

namespace SolutionOrders.Categories.Application.Handlers.Queries;

/// <summary>
/// Handles <see cref="GetAllCategoriesQuery"/> using the Mongo read repository.
/// </summary>
public sealed class GetAllCategoriesHandler(ICategoryReadRepository readRepository)
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await readRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
    }
}

