using Microsoft.Extensions.Configuration;
using Microservices.Ecommerce.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace Microservices.EcommerceApp.ApplicationCore
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
            using var connection = new SqlConnection(connectionString);

            const string query = @"
                DELETE FROM Prodotti WHERE id=@Id
            ";

            return connection.ExecuteAsync(query, new { Id = Id });
            
        }

        public Task<IEnumerable<Prodotto>> GetAll()
        {
            using var connection = new SqlConnection(connectionString);

            const string query = @"
                SELECT
                    [id] as Id
                    [nome] as Nome
                    ,[descrizione] as Descrizione
                    ,[aliquota] as Aliquota
                    ,[marca] as Marca
                    ,[prezzo] as Prezzo
                FROM Prodotti 
            ";

            var list = connection.QueryAsync<Prodotto>(query);
            return list;
        }

        public Task<Prodotto> GetById(int Id)
        {
            using var connection = new SqlConnection(connectionString);

            const string query = @"
                SELECT 
                    [id] as Id
                    [nome] as Nome
                    ,[descrizione] as Descrizione
                    ,[aliquota] as Aliquota
                    ,[marca] as Marca
                    ,[prezzo] as Prezo
                FROM Prodotti WHERE id=@Id
            ";

            var prodotto = connection.QuerySingleAsync<Prodotto>(query, new { Id = Id});
            return prodotto;
        }

        public Task Insert(Prodotto prodotto)
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
            GO
            ";

            return connection.ExecuteAsync(query, prodotto);

        }

        public Task Update(Prodotto prodotto)
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
                GO
            ";

            return connection.ExecuteAsync(query, prodotto);
        }
    }
}
