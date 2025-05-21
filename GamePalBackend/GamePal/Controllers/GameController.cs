using GamePal.Services.GameServices;
using Microsoft.AspNetCore.Mvc;

namespace GamePal.Controllers
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
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
                Console.Error.WriteLine(ex.Message);
                return NotFound();
            }
        }
    }
}
