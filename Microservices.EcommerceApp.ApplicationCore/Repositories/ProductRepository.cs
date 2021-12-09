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
        public async Task Delete(int Id)
        {
            using var connection = new SqlConnection(connectionString);

            const string query = @"
                DELETE FROM Prodotto WHERE id=@Id
            ";

            await connection.ExecuteAsync(query, new { Id = Id });
            
        }

        public Task<IEnumerable<Prodotto>> GetAll()
        {
            var connection = new SqlConnection(connectionString);

            const string query = @"
                SELECT
                    [id] as Id,
                    [nome] as Nome
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

        public async Task<Prodotto> GetById(int Id)
        {
            using var connection = new SqlConnection(connectionString);

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

            var prodotto = await connection.QuerySingleAsync<Prodotto>(query, new { Id = Id});
            return prodotto;
        }

        public async Task Insert(Prodotto prodotto)
        {
            using var connection = new SqlConnection(connectionString);

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
                       ,@Prezzo)
            ";

            await connection.ExecuteAsync(query, prodotto);

        }

        public async Task Update(Prodotto prodotto)
        {
            using var connection = new SqlConnection(connectionString);

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

            await connection.ExecuteAsync(query, prodotto);
        }
    }
}
