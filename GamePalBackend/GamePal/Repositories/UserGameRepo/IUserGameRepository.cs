using GamePal.Data.Entities;
using GamePal.Models.GameModels;

namespace GamePal.Repositories.UserGameRepo
{
    public interface IUserGameRepository
    {
        public Task<IEnumerable<UserGame>> GetAllWithoutUsersAsync();

    }
}
