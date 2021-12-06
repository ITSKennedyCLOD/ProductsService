using Microservices.Ecommerce.DTO;
using Microservices.EcommerceApp.ApplicationCore;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        // GET: api/<ProdottiController>
        [HttpGet("/product/{productId}")]
        public async Task<IEnumerable<Recensione>> Get(int productId)
        {
            var list = await _reviewRepository.GetAll(productId);
            return list;
        }

        // GET api/<ProdottiController>/5
        [HttpGet("{id}")]
        public async Task<Recensione> GetById(int id)
        {
            var recensione = await _reviewRepository.GetById(id);
            return recensione;
        }

        // POST api/<ProdottiController>
        [HttpPost]
        public async Task Post([FromBody] Recensione recensione)
        {
            await _reviewRepository.Insert(recensione);
        }

        // PUT api/<ProdottiController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Recensione recensione)
        {
            await _reviewRepository.Update(recensione);
        }

        // DELETE api/<ProdottiController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _reviewRepository.Delete(id);
        }
    }
}
