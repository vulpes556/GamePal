using GamePal.Services.UserGameServices;
using Microsoft.AspNetCore.Mvc;

namespace GamePal.Controllers
{
    [ApiController]
    [Route("user-games")]
    public class UserGameController : ControllerBase
    {
        private IUserGameService _userGameService;
        private ILogger<UserGameController> _logger;

        public UserGameController(IUserGameService userGameService, ILogger<UserGameController> logger)
        {
            _userGameService = userGameService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllWithoutUsersAsync()
        {
            try
            {
                var gameDTOs = await _userGameService.GetAllWithoutUsersAsync();
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
