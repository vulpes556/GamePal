using GamePal.Models.GameModels;

namespace GamePal.Services.GameServices
{
    public interface IGameService
    {
        public Task<IEnumerable<GameDTO>> GetAllAsync();
    }
}
