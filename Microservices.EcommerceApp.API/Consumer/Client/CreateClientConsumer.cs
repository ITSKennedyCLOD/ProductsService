using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.Client
{
    public class CreateClientConsumer : IConsumer<CreateClientEvent>
    {

        public Task Consume(ConsumeContext<CreateClientEvent> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
