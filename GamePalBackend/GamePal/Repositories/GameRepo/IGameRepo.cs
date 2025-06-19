using GamePal.Data.Entities;
using GamePal.DTOs.GameModels;

namespace GamePal.Repositories.GameRepo
{
    public interface IGameRepo
    {
        public Task<IEnumerable<Game>> GetAllAsync();
        public Task<Game> FindByIdAsync(int id);
        public Task<PagedResult<Game>> GetPagedAsync(int page, int pageSize);


    }

}
