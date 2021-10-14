using GamesRegistrationApi.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRegistrationApi.Repository
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;
        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        public async Task Delete(Guid id)
        {
            var con = $"delete from Game where Id = '{id}'";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(con, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Game>> Get(int page, int quantity)
        {
            var games = new List<Game>();

            var con = $"select * from Game order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(con, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<List<Game>> Get(string name, string producer)
        {
            var games = new List<Game>();

            var con = $"select * from Game where Name = '{name}' and Producer = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(con, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            
            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"], 
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<Game> GetById(Guid id)
        {
            Game game = null;

            var con = $"Select * from Game where Id = '{id}'";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(con, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                };
            }

            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task Insert(Game game)
        {
            var con = $"insert Game (Id, Name, Producer, Price) values ('{game.Id}', '{game.Name}', '{game.Producer}', '{game.Price.ToString().Replace(",", ".")}')";
            
            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(con, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var con = $"update Jogos set Name = '{game.Name}', Producer = '{game.Producer}', Price = {game.Price.ToString().Replace(",", ".")} where Id = '{game.Id}'";

            await sqlConnection.OpenAsync();

            SqlCommand sqlCommand = new SqlCommand(con, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await sqlConnection.CloseAsync();
        }
    }    
}
