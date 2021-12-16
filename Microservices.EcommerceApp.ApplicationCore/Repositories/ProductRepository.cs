using Microsoft.Extensions.Configuration;
using Microservices.Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;

namespace Microservices.EcommerceApp.ApplicationCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Storage");
        }
        public Task Delete(int Id)
        {
            var connection = new SqlConnection(connectionString);

            const string query = @"
                DELETE FROM Prodotto WHERE id=@Id
            ";

            return connection.ExecuteAsync(query, new { Id = Id })
                .ContinueWith(x =>
                {
                    connection.Dispose();
                    return x.Result;
                });
            
        }

        public Task<IEnumerable<Prodotto>> GetAll()
        {
            var connection = new SqlConnection(connectionString);

            const string query = @"
                SELECT
                    [id] as Id
                    ,[nome] as Nome
                    ,[descrizione] as Descrizione
                    ,[aliquota] as Aliquota
                    ,[marca] as Marca
                    ,[prezzo] as Prezzo
                FROM Prodotto
            ";
            return connection.QueryAsync<Prodotto>(query)
                .ContinueWith(x =>
                {
                    connection.Dispose();
                    return x.Result;
                });
        }

        public Task<Prodotto> GetById(int Id)
        {
            var connection = new SqlConnection(connectionString);

            const string query = @"
                SELECT 
                    [id] as Id,
                    [nome] as Nome
                    ,[descrizione] as Descrizione
                    ,[aliquota] as Aliquota
                    ,[marca] as Marca
                    ,[prezzo] as Prezo
                FROM Prodotto WHERE id=@Id
            ";


            return connection.QuerySingleAsync<Prodotto>(query, new { Id = Id })
                .ContinueWith(x =>
                {
                    connection.Dispose();
                    return x.Result;
                });
        }

        public Task<int> Insert(Prodotto prodotto)
        {
            var connection = new SqlConnection(connectionString);

            const string query = @"
                INSERT INTO [dbo].[prodotto]
                       ([nome]
                       ,[descrizione]
                       ,[aliquota]
                       ,[marca]
                       ,[prezzo])
                 VALUES
                       (@Nome
                       ,@Descrizione
                       ,@Aliquota
                       ,@Marca
                       ,@Prezzo);
                SELECT SCOPE_IDENTITY();
            ";


            return connection.ExecuteScalarAsync<int>(query, prodotto).ContinueWith(x => 
            {
                connection.Dispose();
                return x.Result;
            });

        }

        public Task Update(Prodotto prodotto)
        {
            var connection = new SqlConnection(connectionString);

            const string query = @"
                UPDATE [dbo].[prodotto]
                   SET [nome] = @Nome
                      ,[descrizione] = @Descrizione
                      ,[aliquota] = @Aliquota
                      ,[marca] = @Marca
                      ,[prezzo] = @Prezzo
                 WHERE 
                    id=@Id
            ";


            return connection.ExecuteAsync(query, prodotto)
                .ContinueWith(x =>
                {
                    connection.Dispose();

                    return x.Result;
                });
        }
    }
}
