using GamePal.Context;
using GamePal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Repositories.UserAuthProviders
{
    public class UserAuthProviderRepo : IUserAuthProviderRepo
    {
        private readonly DBContext _dbContext;


        public UserAuthProviderRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserAuthProvider> AddUserAuthProviderAsync(UserAuthProvider userAuthProvider)
        {
            await _dbContext.UserAuthProviders.AddAsync(userAuthProvider);
            await _dbContext.SaveChangesAsync();
            return userAuthProvider;
        }

        public async Task<IEnumerable<UserAuthProvider>> GetAllAsync()
        {
            return await _dbContext.UserAuthProviders
                .Include(p => p.User)
                .Include(p => p.AuthProvider)
                .ToListAsync();
        }

        public async Task<User?> GetUserByProviderIdAsync(string providerUserId, string providerName)
        {
            var userByProvider = await _dbContext.UserAuthProviders
                .Include(p => p.User)
                .Include(p => p.AuthProvider)
                .Where(p => p.AuthProvider.Name == providerName && p.ProviderUserId == providerUserId)
                .Select(p => p.User)
                .FirstOrDefaultAsync();

            return userByProvider;
        }
    }
}

