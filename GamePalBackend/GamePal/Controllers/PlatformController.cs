using GamePal.Services.CategoryService;
using GamePal.Services.PlatformService;
using Microsoft.AspNetCore.Mvc;

namespace GamePal.Controllers
{
    [ApiController]
    [Route("platforms")]
    public class PlatformController : ControllerBase
    {
        private ILogger<PlatformController> _logger;
        private IPlatformService _platformService;



        public PlatformController(IPlatformService platformService, ILogger<PlatformController> logger)
        {
            _platformService = platformService;
            _logger = logger;
        }


        [HttpGet()]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            try
            {
                var result = await _platformService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }
    }
}
