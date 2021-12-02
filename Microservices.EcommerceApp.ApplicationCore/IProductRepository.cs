﻿using Microservices.Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.ApplicationCore
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Prodotto>> GetAll();
        public Task<Prodotto> GetById(int Id);
        public Task Insert(Prodotto prodotto);
        public Task Update(Prodotto prodotto);
        public Task Delete(int Id);

    }
}
