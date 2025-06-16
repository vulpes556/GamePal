using GamePal.Data.Entities;

namespace GamePal.Repositories.PlatformRepo
{
    public interface IPlatformRepo
    {
        public Task<Platform> FindByIdAsync(int id);
    }
}
