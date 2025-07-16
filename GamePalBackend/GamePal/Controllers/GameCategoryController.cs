using GamePal.Services.CategoryService;
using GamePal.Services.GameServices;
using Microsoft.AspNetCore.Mvc;

namespace GamePal.Controllers
{
    [ApiController]
    [Route("categories")]
    public class GameCategoryController : ControllerBase
    {
        private ILogger<GameCategoryController> _logger;
        private ICategoryService _categoryService;



        public GameCategoryController(ICategoryService categoryService, ILogger<GameCategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }


        [HttpGet()]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            try
            {
                var result = await _categoryService.GetAllAsync();
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