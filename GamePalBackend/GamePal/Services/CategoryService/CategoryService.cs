using GamePal.Data.Entities;
using GamePal.DTOs;
using GamePal.Models.GameModels;
using GamePal.Repositories.GameCategoryRepo;

namespace GamePal.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        IGameCategoryRepo _categoryRepository;

        public CategoryService(IGameCategoryRepo categoryRepo)
        {
            _categoryRepository = categoryRepo;
        }

        public async Task<IEnumerable<GameCategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(ToDTO);
        }



        private GameCategoryDTO ToDTO(GameCategory category)
        {
            return new GameCategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

    }
}