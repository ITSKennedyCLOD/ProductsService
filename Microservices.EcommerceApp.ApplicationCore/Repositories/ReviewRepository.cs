using Dapper;
using Microservices.Ecommerce.DTO;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.ApplicationCore.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string connectionString;
        public ReviewRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Storage");
        }
        public Task Delete(int Id)
        {
            using var connection = new SqlConnection(connectionString);

            const string query = @"
                DELETE FROM Recensioni WHERE id=@Id
            ";

            return connection.ExecuteAsync(query, new { Id = Id })
                .ContinueWith(x =>
                {
                    connection.Dispose();

                    return x.Result;
                });
        }

        public Task<IEnumerable<Recensione>> GetAll(int Id)
        {
            using var connection = new SqlConnection(connectionString);

            const string query = @"

                SELECT [R.id] as Id
                      ,[R.voto] as Voto
                      , [C.Nome] as Cliente
                      ,[R.descrizione] as Descrizione
                      ,[R.id_prodotto_ordine] as IdOrdineProdotto
                  FROM [dbo].[recensione] R
                INNER JOIN
                    [dbo].[ordine_prodotto] OP
                ON
                    OP.id=R.id_prodotto_ordine
                INNER JOIN
                    [dbo].[ordini] O
                ON
                    OP.id_ordine=O.id
                INNER JOIN
                    [dbo].[clienti] C
                ON
                    O.id_cliente=C.id
                WHERE
                    OP.id=@Id
                    
            ";

            return connection.QueryAsync<Recensione>(query, new { Id = Id })
                .ContinueWith(x =>
                {
                    connection.Dispose();

                    return x.Result;
                });

        }

        public Task<Recensione> GetById(int Id)
        {
            using var connection = new SqlConnection(connectionString);

            const string query = @"
                SELECT [R.id] as Id
                      ,[R.voto] as Voto
                      , [C.Nome] as Cliente
                      ,[R.descrizione] as Descrizione
                      ,[R.id_prodotto_ordine] as IdOrdineProdotto
                  FROM [dbo].[recensione] R
                INNER JOIN
                    [dbo].[ordine_prodotto] OP
                ON
                    OP.id=R.id_prodotto_ordine
                INNER JOIN
                    [dbo].[ordini] O
                ON
                    OP.id_ordine=O.id
                INNER JOIN
                    [dbo].[clienti] C
                ON
                    O.id_cliente=C.id
                WHERE
                    OP.id=@Id
                WHERE
                    R.id=@Id
            ";

            return connection.QuerySingleAsync<Recensione>(query, new { Id = Id })
                .ContinueWith(x =>
                {
                    connection.Dispose();

                    return x.Result;
                });
        }

        public Task Insert(Recensione recensione)
        {
            using var connection = new SqlConnection(connectionString);

            const string query = @"
                INSERT INTO [dbo].[recensione]
                       ,[voto]
                       ,[descrizione])
                 VALUES
                       @Voto
                       @Descrizione
            ";

            return connection.ExecuteAsync(query, recensione)
                .ContinueWith(x =>
                {
                    connection.Dispose();

                    return x.Result;
                });
        }

        public Task Update(Recensione recensione)
        {
            using var connection = new SqlConnection(connectionString);

            const string query = @"
                UPDATE [dbo].[recensione]
                   SET [id] = @Id
                      ,[voto] = @Voto
                      ,[descrizione] = @Descrizione
                 WHERE 
                    id=@Id
            ";

            return connection.ExecuteAsync(query, recensione)
                .ContinueWith(x =>
                {
                    connection.Dispose();

                    return x.Result;
                });
        }
    }
}
