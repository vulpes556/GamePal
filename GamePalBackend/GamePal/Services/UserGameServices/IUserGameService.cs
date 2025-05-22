using GamePal.Models.GameModels;

namespace GamePal.Services.UserGameServices
{
    public interface IUserGameService
    {
        public Task<IEnumerable<GameDTO>> GetAllWithoutUsersAsync();

    }
}
