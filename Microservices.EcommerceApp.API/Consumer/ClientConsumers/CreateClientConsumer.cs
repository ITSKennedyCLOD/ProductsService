using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Gruppo3.ClientiDTO.Domain.Entities;
using Microservices.Ecommerce.DTO.Commands;

namespace Microservices.EcommerceApp.API.Consumer.ClientConsumers
{
    public class CreateClientConsumer : IConsumer<CreateClientCommands>
    {
        private readonly IClientRepository _clientRepository;
        public CreateClientConsumer(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Consume(ConsumeContext<CreateClientCommands> context)
        {
            var client = new Client 
            {
                Name = context.Message.Name,
              
                Businessname = context.Message.Businessname,
       
                Id = context.Message.Id 
            };
            
            await _clientRepository.CreateClient(client);
        }
    }
}
