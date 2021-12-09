using Gruppo4MicroserviziDTO.DTOs;
using Gruppo4MicroserviziDTO.Models;
using MassTransit;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.Order
{
    public class NewOrderConsumer : IConsumer<NewOrderEvent>
    {

        private readonly IOrderRepository _orderRepository;

        public NewOrderConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task Consume(ConsumeContext<NewOrderEvent> context)
        {
            return _orderRepository.CreateOrder(context.Message);
        }
    }
}
