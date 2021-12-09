using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.Client
{
    public class DeleteClientConsumer : IConsumer<DeleteClientEvent>
    {
        public Task Consume(ConsumeContext<DeleteClientEvent> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
