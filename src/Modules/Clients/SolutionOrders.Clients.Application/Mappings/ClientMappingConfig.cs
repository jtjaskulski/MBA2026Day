using Mapster;
using SolutionOrders.Clients.Application.Messages.Commands;
using SolutionOrders.Clients.Application.Messages.DTOs;
using SolutionOrders.Clients.Domain.Entities;

namespace SolutionOrders.Clients.Application.Mappings
{
    public class ClientMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Client, ClientDto>();

            config.NewConfig<CreateClientCommand, Client>()
                .Map(dest => dest.IsActive, _ => true)
                .Ignore(dest => dest.IdClient);

            config.NewConfig<UpdateClientCommand, Client>()
                .Ignore(dest => dest.IdClient);
        }
    }
}



