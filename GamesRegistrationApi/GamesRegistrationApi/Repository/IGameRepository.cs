using GamesRegistrationApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRegistrationApi.Repository
{
    public interface IGameRepository
    {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> GetById(Guid id);
        Task<List<Game>> Get(string name, string producer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
