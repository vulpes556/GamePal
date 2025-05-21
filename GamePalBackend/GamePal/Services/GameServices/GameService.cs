using GamePal.Models.GameModels;

namespace GamePal.Services.GameServices
{
    public class GameService : IGameService
    {
        public Task<IEnumerable<GameDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
