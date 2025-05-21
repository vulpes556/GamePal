using GamePal.Data.Entities;
using GamePal.Models.GameModels;
using GamePal.Repositories.GameRepo;

namespace GamePal.Services.GameServices
{
    public class GameService : IGameService
    {

        IGameRepo _gameRepository;

        public GameService(IGameRepo gameRepo)
        {
            _gameRepository = gameRepo;
        }

        public Task<IEnumerable<GameDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }

}
