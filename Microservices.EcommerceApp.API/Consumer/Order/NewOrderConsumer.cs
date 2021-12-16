using Gruppo4MicroserviziDTO.DTOs;
using Gruppo4MicroserviziDTO.Models;
using MassTransit;
using Microservices.Ecommerce.DTO.Commands;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.Order
{
    public class NewOrderConsumer : IConsumer<CreateOrderCommand>
    {

        private readonly IOrderRepository _orderRepository;

        public NewOrderConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task Consume(ConsumeContext<CreateOrderCommand> context)
        {
            var list = new List<Gruppo4MicroserviziDTO.Models.ProductInOrder>();
            foreach (var i in context.Message.Products)
            {
                list.Add(new Gruppo4MicroserviziDTO.Models.ProductInOrder
                {
                    OrderedQuantity = i.OrderedQuantity,
                    ProductId = i.ProductId
                });
            }
            return _orderRepository.UpdateOrder(new UpdatedOrderEvent
            {
                Id = context.Message.Id,
                DiscountAmount = context.Message.DiscountAmount,
                DiscountedPrice = context.Message.DiscountedPrice,
                IdCliente = context.Message.IdCliente,
                Products = list,
                TotalPrice = context.Message.TotalPrice
            });
        }
    }
}
