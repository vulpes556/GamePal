using GamePal.DTOs.GameModels;
using GamePal.Models.GameModels;

namespace GamePal.Services.GameServices
{
    public interface IGameService
    {
        public Task<IEnumerable<GameDTO>> GetAllAsync();
        public Task<PagedResult<GameDTO>> GetGamesAsync(int page, int pageSize, string? genre, string? name, string? platform);

    }
}
