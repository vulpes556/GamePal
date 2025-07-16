using GamePal.Data.Entities;

namespace GamePal.Repositories.GameCategoryRepo
{
    public interface IGameCategoryRepo
    {
        public Task<IEnumerable<GameCategory>> GetAllAsync();

    }
}
