using Mapster;
using SolutionOrders.API.Features.Categories.Messages.DTOs;
using SolutionOrders.API.Models;

namespace SolutionOrders.API.Features.Categories.Mappings
{
    public class CategoryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CategoryDto>();
        }
    }
}