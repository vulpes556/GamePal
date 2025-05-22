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
                .Include(ug => ug.Platform)
                .Include(ug => ug.Game)
                .ThenInclude(g => g.Categories)
                .ToListAsync();
        }
    }
}
