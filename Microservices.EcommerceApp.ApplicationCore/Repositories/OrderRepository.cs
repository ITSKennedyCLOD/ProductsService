using Dapper;
using Gruppo4MicroserviziDTO.DTOs;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.ApplicationCore.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Storage");
        }


        public async Task CreateOrder(NewOrderEvent order)
        {

            using var connection = new SqlConnection(_connectionString);

            const string query = @"
                INSERT INTO [dbo].[ordine]
                       ([id], [idCliente])
                 VALUES
                       (@Id, @IdCliente)
            ";


            await connection.ExecuteAsync(query, order);

        }


        public async Task UpdateOrder(UpdatedOrderEvent order)
        {

            using var connection = new SqlConnection(_connectionString);

            const string query = @"
                UPDATE [dbo].[ordine]
                   SET [id] = @Id, [idCliente] = @IdCliente
                 WHERE id=@Id
            ";


            await connection.ExecuteAsync(query, order);

        }


        public async Task DeleteOrder(DeletedOrderEvent order)
        {

            using var connection = new SqlConnection(_connectionString);

            const string query = @"
                DELETE FROM ordine where id=@Id
            ";


            await connection.ExecuteAsync(query, order);

        }



    }
}
