using GamePal.Data.Entities;
using GamePal.DTOs;
using GamePal.DTOs.GameModels;
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
            return games.Select(ToDTO);
        }

        public async Task<PagedResult<GameDTO>> GetGamesAsync(int page, int pageSize)
        {
          var result = await _gameRepository.GetPagedAsync(page, pageSize);

            return new PagedResult<GameDTO>()
            {
                Items = result.Items.Select(ToDTO),
                TotalCount = result.TotalCount,
            };
        }

        private GameDTO ToDTO(Game game)
        {
            return new GameDTO()
            {
                GameId = game.Id,
                Name = game.Name,
                PictureUrl = game.ImageUrl,
                Categories = game.Categories.Select(g => new GameCategoryDTO()
                {
                    Id = g.Id,
                    Name = g.Name,
                }
                ).ToList(),
                Platforms = game.Platforms.Select(g => new PlatformDTO()
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToList(),
            };
        }



    }


}
