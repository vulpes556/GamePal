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

        public async Task<IEnumerable<UserGameDTO>> GetAllWithoutUsersAsync()
        {
            var userGames = await _userGameRepository.GetAllWithoutUsersAsync();
            return userGames.Select(ToGameDTO);
        }

        private UserGameDTO ToGameDTO(UserGame userGame)
        {
            return new UserGameDTO
            {
                GameId = userGame.Game.Id,
                GameName = userGame.Game.Name,
                Platform = userGame.Platform.Name,
                Categories = userGame.Game.Categories
                     .Select(c => c.Name)
                     .ToList()
            };
        }
    }
}
