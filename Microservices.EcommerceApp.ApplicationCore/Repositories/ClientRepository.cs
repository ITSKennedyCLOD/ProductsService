using Dapper;
using Gruppo3.ClientiDTO.Domain.Entities;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.ApplicationCore.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private readonly string _connectionString;

        public ClientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Storage");
        }


        public Task CreateClient(Client client)
        {
            using var connection = new SqlConnection(_connectionString);

            const string query = @"
                
            ";

            return connection.ExecuteAsync(query, new { });
        }

        public Task DeleteClient(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            const string query = @"
                DELETE FROM clienti WHERE id=@Id
            ";

            return connection.ExecuteAsync(query, new { Id = id });
        }

        public Task UpdateClient(Client client)
        {
            using var connection = new SqlConnection(_connectionString);

            const string query = @"
                
            ";

            return connection.ExecuteAsync(query, new { });
        }
    }
}
