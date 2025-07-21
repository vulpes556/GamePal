using GamePal.DTOs;
using GamePal.Models.GameModels;

namespace GamePal.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<IEnumerable<GameCategoryDTO>> GetAllAsync();


    }
}
