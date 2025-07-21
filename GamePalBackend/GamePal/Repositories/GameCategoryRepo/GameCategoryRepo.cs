using GamePal.Context;
using GamePal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Repositories.GameCategoryRepo
{
    public class GameCategoryRepo : IGameCategoryRepo
    {

        private readonly DBContext _dbContext;

        public GameCategoryRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<GameCategory>> GetAllAsync()
        {
            return await _dbContext.GameCategories.ToListAsync();

        }
    }
}
