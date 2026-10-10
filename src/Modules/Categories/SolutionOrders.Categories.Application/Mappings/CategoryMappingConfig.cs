using Mapster;
using SolutionOrders.Categories.Application.Messages.DTOs;
using SolutionOrders.Categories.Domain.Entities;

namespace SolutionOrders.Categories.Application.Mappings
{
    public class CategoryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CategoryDto>();
        }
    }
}
