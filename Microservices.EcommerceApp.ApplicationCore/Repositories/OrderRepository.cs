using Dapper;
using Gruppo4MicroserviziDTO.DTOs;
using Gruppo4MicroserviziDTO.Models;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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


            foreach (var product in order.Products)
            {
                const string queryOrderProduct = @"
                    INSERT INTO [dbo].[ordine_prodotto]
                           ([id_prodotto]
                           ,[id_ordine]
                    )
                     VALUES
                           (@ProductId
                           ,@Id
                           )
                ";


                connection.Execute(queryOrderProduct, new { ProductId = product.ProductId, Id = order.Id });
            }


        }

        public async Task UpdateOrder(UpdatedOrderEvent order)
        {

            using var connection = new SqlConnection(_connectionString);

            const string query = @"
                UPDATE [dbo].[ordine]
                   SET [id] = @Id, [idCliente] = @IdCliente
                 WHERE id=@Id;
                DELETE FROM ordine_prodotto where id_ordine=@Id;
            ";


            await connection.ExecuteAsync(query, order);

            foreach (var product in order.Products)
            {
                const string queryOrderProduct = @"
                    INSERT INTO [dbo].[ordine_prodotto]
                           ([id_prodotto]
                           ,[id_ordine]
                    )
                     VALUES
                        (
                            @ProductId
                           ,@Id
                        )
                ";


                connection.Execute(queryOrderProduct, new { ProductId = product.ProductId, Id = order.Id });
            }

        }


        public async Task DeleteOrder(DeletedOrderEvent order)
        {

            using var connection = new SqlConnection(_connectionString);

            const string query = @"
                DELETE FROM ordine_prodotto where id=@Id;
                DELETE FROM ordine where id_ordine=@Id;
            ";


            await connection.ExecuteAsync(query, order);

        }



    }
}
