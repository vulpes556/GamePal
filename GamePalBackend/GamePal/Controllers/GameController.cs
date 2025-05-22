using GamePal.Services.GameServices;
using Microsoft.AspNetCore.Mvc;

namespace GamePal.Controllers
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {
        private IGameService _gameService;
        private ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var gameDTOs = await _gameService.GetAllAsync();
                return Ok(gameDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }
    }
}
