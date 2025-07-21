using GamePal.DTOs;

namespace GamePal.Services.PlatformService
{
    public interface IPlatformService
    {
        public Task<IEnumerable<PlatformDTO>> GetAllAsync();

    }
}
