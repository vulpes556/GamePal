using GamePal.Context;
using GamePal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Repositories.GameRepo
{
    public class GameRepo : IGameRepo
    {
        private readonly DBContext _dbContext;

        public GameRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Game> FindByIdAsync(int id)
        {
            return await _dbContext.Games
                .Include(g => g.Platforms)
                .Include(g => g.Categories)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _dbContext.Games
                .Include(g => g.Categories)
                .Include(g => g.Platforms)
                .ToListAsync();
        }
    }
}
