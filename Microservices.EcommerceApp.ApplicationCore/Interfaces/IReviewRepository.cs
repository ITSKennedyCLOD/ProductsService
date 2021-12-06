using Microservices.Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.ApplicationCore.Interfaces
{
    public interface IReviewRepository
    {
        public Task<IEnumerable<Recensione>> GetAll(int Id);
        public Task<Recensione> GetById(int Id);
        public Task Insert(Recensione Recensione);
        public Task Update(Recensione Recensione);
        public Task Delete(int Id);
    }
}
