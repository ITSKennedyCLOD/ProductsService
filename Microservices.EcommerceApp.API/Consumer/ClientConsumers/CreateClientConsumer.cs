using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using Gruppo3.ClientiDTO.Domain.Entities;


namespace Microservices.EcommerceApp.API.Consumer.ClientConsumers
{
    public class CreateClientConsumer : IConsumer<CreateClientEvent>
    {
        private readonly IClientRepository _clientRepository;
        public CreateClientConsumer(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Consume(ConsumeContext<CreateClientEvent> context)
        {
            var client = new Client 
            {
                Name = context.Message.Name,
                Surname = context.Message.Surname,
                Address = context.Message.Address,
                Businessname = context.Message.Businessname,
                CF = context.Message.CF,
                Email = context.Message.Email,
                Piva = context.Message.Piva,
                Year = context.Message.Year,
                Id = context.Message.Id
            };
            
            await _clientRepository.CreateClient(client);
        }
    }
}
