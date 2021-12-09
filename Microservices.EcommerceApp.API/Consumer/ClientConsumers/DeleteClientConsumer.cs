using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.ClientConsumers
{
    public class DeleteClientConsumer : IConsumer<DeleteClientEvent>
    {
        private readonly IClientRepository _clientRepository;
        public DeleteClientConsumer(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Consume(ConsumeContext<DeleteClientEvent> context)
        {

            
            await _clientRepository.DeleteClient(context.Message.Id);
            

        }
    }
}
