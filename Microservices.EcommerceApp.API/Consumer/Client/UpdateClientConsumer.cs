using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.Client
{
    public class UpdateClientConsumer : IConsumer<UpdateClientEvent>
    {
        public Task Consume(ConsumeContext<UpdateClientEvent> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
