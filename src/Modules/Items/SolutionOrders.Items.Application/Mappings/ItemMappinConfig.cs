using Mapster;
using SolutionOrders.Items.Application.Messages.Commands;
using SolutionOrders.Items.Application.Messages.DTOs;
using SolutionOrders.Items.Domain.Entities;

namespace SolutionOrders.Items.Application.Mappings
{
    public class ItemMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Item, ItemDto>()
                .Map(dest => dest.CategoryName, src => src.Category.Name)
                .Map(dest => dest.UnitName, src => src.UnitOfMeasurement != null 
                    ? src.UnitOfMeasurement.Name : null);

            config.NewConfig<CreateItemCommand, Item>()
                .Map(dest => dest.IsActive, _ => true)
                .Ignore(dest => dest.IdItem)
                .Ignore(dest => dest.Category)
                .Ignore(dest => dest.UnitOfMeasurement!);
            
            config.NewConfig<UpdateItemCommand, Item>()
                .Ignore(dest => dest.IdItem)
                .Ignore(dest => dest.Category)
                .Ignore(dest => dest.UnitOfMeasurement!);
        }
    }
}

