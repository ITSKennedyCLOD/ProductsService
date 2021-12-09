using Gruppo4MicroserviziDTO.Models;
using System;
using System.Collections.Generic;

namespace Gruppo4MicroserviziDTO.DTOs
{
    public class NewOrderEvent
    {
        public Guid Id { get; set; }
        public int IdCliente { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountedPrice { get; set; }

        public IEnumerable<ProductInOrder> Products { get; set; }
    }
}