using GamePal.Data.Entities;

namespace GamePal.Repositories.GameRepo
{
    public interface IGameRepo
    {
        public Task<IEnumerable<Game>> GetAllAsync();

    }

}
