using GamesRegistrationApi.InputModel;
using GamesRegistrationApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRegistrationApi.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Get(int page, int quantity);
        Task<GameViewModel> GetById(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Delete(Guid id);
    }
}
