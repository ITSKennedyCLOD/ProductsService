using Gruppo3.ClientiDTO.Domain.Entities;
using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using Microservices.Ecommerce.DTO.Commands;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.ClientConsumers
{
    public class UpdateClientConsumer : IConsumer<UpdateClientCommands>
    {
        private readonly IClientRepository _clientRepository;
        public UpdateClientConsumer(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        public async Task Consume(ConsumeContext<UpdateClientCommands> context)
        {
            var client = new Client
            {
                Name = context.Message.Name,
         
                Businessname = context.Message.Businessname,


                Id = context.Message.Id
            };


            await _clientRepository.UpdateClient(client);
        }
    }
}
