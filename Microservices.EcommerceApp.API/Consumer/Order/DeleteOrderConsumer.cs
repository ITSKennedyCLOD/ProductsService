using Gruppo4MicroserviziDTO.DTOs;
using MassTransit;
using Microservices.Ecommerce.DTO.Commands;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.Order
{
    public class DeleteOrderConsumer : IConsumer<DeleteOrderCommand>
    {

        private readonly IOrderRepository _orderRepository;

        public DeleteOrderConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task Consume(ConsumeContext<DeleteOrderCommand> context)
        {
            return _orderRepository.DeleteOrder(new DeletedOrderEvent
            { Id=context.Message.Id
            });
        }
    }
}
