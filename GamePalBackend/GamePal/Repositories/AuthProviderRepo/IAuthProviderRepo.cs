using GamePal.Data.Entities;

namespace GamePal.Repositories.AuthProviderRepo
{
    public interface IAuthProviderRepo
    {
        Task<AuthProvider> AddProviderAsync(AuthProvider authProvider);

    }
}
