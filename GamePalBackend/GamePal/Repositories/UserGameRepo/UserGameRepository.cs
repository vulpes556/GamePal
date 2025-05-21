using GamePal.Context;
using GamePal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Repositories.UserGameRepo
{
    public class UserGameRepository : IUserGameRepository
    {
        private readonly DBContext _dbContext;

        public UserGameRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<UserGame>> GetAllWithoutUsersAsync()
        {
            return await _dbContext.UserGames
                .Include(g => g.Platform)
                .Include(g => g.Game)
                .ToListAsync();
        }
    }
}
