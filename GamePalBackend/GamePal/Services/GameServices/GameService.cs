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
        public async Task<IEnumerable<GameDTO>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return games.Select(ModelToDTOConverter);
        }



        private GameDTO ModelToDTOConverter(Game game)
        {
            return new GameDTO()
            {
                Id = game.Id,
                Name = game.Name,
                Platform = game.Platform,
            };
        }
    }

}
