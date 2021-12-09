
using Gruppo3.ClientiDTO.Domain.Entities;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.ApplicationCore.Interfaces
{
    public interface IClientRepository
    {
        public Task CreateClient(Client client);

        public Task UpdateClient(Client client);

        public Task DeleteClient(int id);
    }
}
