using MassTransit;
using Microservices.Ecommerce.DTO;
using Microservices.EcommerceApp.ApplicationCore;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Ecommerce.DTO.Events;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.EcommerceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public ProdottiController(IProductRepository productRepository, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
        }
        // GET: api/<ProdottiController>
        [HttpGet]
        public Task<IEnumerable<Prodotto>> Get()
        {
            var list = _productRepository.GetAll();
            return list;
        }

        // GET api/<ProdottiController>/5
        [HttpGet("{id}")]
        public Task<Prodotto> Get(int id)
        {
            var prodotto = _productRepository.GetById(id);
            return prodotto;
        }

        // POST api/<ProdottiController>
        [HttpPost]
        public async Task Post([FromBody] Prodotto prodotto)
        {
            var id = await _productRepository.Insert(prodotto);

            await _publishEndpoint.Publish<NewProductEvent>(new NewProductEvent 
            {   
                Id = id,
                Aliquota = prodotto.Aliquota,
                Marca = prodotto.Marca,
                Nome = prodotto.Nome,
                Descrizione = prodotto.Descrizione,
                Prezzo = prodotto.Prezzo
            });
        }

        // PUT api/<ProdottiController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Prodotto prodotto)
        {
            prodotto.Id = id;

            await _productRepository.Update(prodotto);

            await _publishEndpoint.Publish<NewProductEvent>(new UpdateProductEvent
            {
                Id = prodotto.Id,
                Marca = prodotto.Marca,
                Nome = prodotto.Nome,
                Descrizione = prodotto.Descrizione,
                Prezzo = prodotto.Prezzo
            });
        }

        // DELETE api/<ProdottiController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {

            await _productRepository.Delete(id);

            await _publishEndpoint.Publish<NewProductEvent>(new DeleteProductEvent
            {
                Id = id
            });
        }
    }
}
