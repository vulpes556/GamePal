using GamePal.Data.Entities;
using GamePal.DTOs;
using GamePal.Repositories.PlatformRepo;

namespace GamePal.Services.PlatformService
{
    public class PlatformService : IPlatformService
    {
        IPlatformRepo _platformRepository;

        public PlatformService(IPlatformRepo platformRepository)
        {
            _platformRepository = platformRepository;
        }

        public async Task<IEnumerable<PlatformDTO>> GetAllAsync()
        {
            var platforms = await _platformRepository.GetAllAsync();
            return platforms.Select(ToDTO);
        }



        private PlatformDTO ToDTO(Platform platform)
        {
            return new PlatformDTO()
            {
                Id = platform.Id,
                Name = platform.Name,
            };
        }

    }
}
