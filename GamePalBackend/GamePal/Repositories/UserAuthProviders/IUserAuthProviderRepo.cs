using GamePal.Data.Entities;

namespace GamePal.Repositories.UserAuthProviders
{
    public interface IUserAuthProviderRepo
    {
        Task<IEnumerable<UserAuthProvider>> GetAllAsync();
        Task<User?> GetUserByProviderIdAsync(string providerUserId, string providerName);
        Task<UserAuthProvider> AddUserAuthProviderAsync(UserAuthProvider userAuthProvider);

    }
}
