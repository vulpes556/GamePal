using GamePal.Data.Entities;
using GamePal.Models.GameModels;
using GamePal.Repositories.UserGameRepo;

namespace GamePal.Services.UserGameServices
{
    public class UserGameService : IUserGameService
    {
        IUserGameRepository _userGameRepository;
        public UserGameService(IUserGameRepository userGameRepository)
        {
            _userGameRepository = userGameRepository;
        }

        public async Task<IEnumerable<GameDTO>> GetAllWithoutUsersAsync()
        {
            var userGames = await _userGameRepository.GetAllWithoutUsersAsync();
            return userGames.Select(ConvertGameToDTO);
        }

        private GameDTO ConvertGameToDTO(UserGame userGame)
        {
            return new GameDTO
            {
                GameId = userGame.Game.Id,
                Name = userGame.Game.Name,
                Platform = userGame.Platform.Name,
            };
        }
    }
}
