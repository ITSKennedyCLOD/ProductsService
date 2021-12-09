using Gruppo4MicroserviziDTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.ApplicationCore.Interfaces
{
    public interface IOrderRepository
    {
        public Task CreateOrder(NewOrderEvent order);

        public Task UpdateOrder(UpdatedOrderEvent order);

        public Task DeleteOrder(DeletedOrderEvent order);
    }
}
