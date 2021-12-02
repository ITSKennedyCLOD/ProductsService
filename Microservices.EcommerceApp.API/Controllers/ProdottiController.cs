using Microservices.Ecommerce.DTO;
using Microservices.EcommerceApp.ApplicationCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Microservices.EcommerceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProdottiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/<ProdottiController>
        [HttpGet]
        public async Task<IEnumerable<Prodotto>> Get()
        {
            var list = await _productRepository.GetAll();
            return list;
        }

        // GET api/<ProdottiController>/5
        [HttpGet("{id}")]
        public async Task<Prodotto> Get(int id)
        {
            var prodotto = await _productRepository.GetById(id);
            return prodotto;
        }

        // POST api/<ProdottiController>
        [HttpPost]
        public async Task Post([FromBody] Prodotto prodotto)
        {
            await _productRepository.Insert(prodotto);
        }

        // PUT api/<ProdottiController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Prodotto prodotto)
        {
            await _productRepository.Update(prodotto);
        }

        // DELETE api/<ProdottiController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productRepository.Delete(id);
        }
    }
}
