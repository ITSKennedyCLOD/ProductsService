using Gruppo4MicroserviziDTO.DTOs;
using MassTransit;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.Order
{
    public class UpdateOrderConsumer : IConsumer<UpdatedOrderEvent>
    {

        private readonly IOrderRepository _orderRepository;

        public UpdateOrderConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task Consume(ConsumeContext<UpdatedOrderEvent> context)
        {
            return _orderRepository.UpdateOrder(context.Message);
        }
    }
}
