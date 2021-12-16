using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using Microservices.Ecommerce.DTO.Commands;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.ClientConsumers
{
    public class DeleteClientConsumer : IConsumer<DeleteClientCommands>
    {
        private readonly IClientRepository _clientRepository;
        public DeleteClientConsumer(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Consume(ConsumeContext<DeleteClientCommands > context)
        {

            
            await _clientRepository.DeleteClient(context.Message.Id);
            

        }
    }
}
