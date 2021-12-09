using Gruppo4MicroserviziDTO.DTOs;
using MassTransit;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.Order
{
    public class DeleteOrderConsumer : IConsumer<DeletedOrderEvent>
    {

        private readonly IOrderRepository _orderRepository;

        public DeleteOrderConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task Consume(ConsumeContext<DeletedOrderEvent> context)
        {
            return _orderRepository.DeleteOrder(context.Message);
        }
    }
}
