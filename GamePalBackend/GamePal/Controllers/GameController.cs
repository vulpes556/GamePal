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
        public async Task<IActionResult> GetGamesAsync( [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pagedResult = await _gameService.GetGamesAsync(page, pageSize);
                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound();
            }
        }
    }
}
