using GamePal.Context;
using GamePal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Repositories.PlatformRepo
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly DBContext _dbContext;

        public PlatformRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            return await _dbContext.Platforms.ToListAsync();
        }
    }
}
