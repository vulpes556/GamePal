
using GamePal.Context;
using GamePal.Data.Entities;

namespace GamePal.Repositories.AuthProviderRepo
{
    public class AuthProviderRepo : IAuthProviderRepo
    {
        private readonly DBContext _dbContext;

        public AuthProviderRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthProvider> AddProviderAsync(AuthProvider authProvider)
        {
            await _dbContext.AuthProviders.AddAsync(authProvider);
            await _dbContext.SaveChangesAsync();
            return authProvider;
        }
    }
}
