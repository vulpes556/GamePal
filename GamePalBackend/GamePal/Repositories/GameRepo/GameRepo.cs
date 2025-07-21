using GamePal.Context;
using GamePal.Data.Entities;
using GamePal.DTOs.GameModels;
using GamePal.Models.GameModels;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Repositories.GameRepo
{
    public class GameRepo : IGameRepo
    {
        private readonly DBContext _dbContext;

        public GameRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Game> FindByIdAsync(int id)
        {
            return await _dbContext.Games
                .Include(g => g.Platforms)
                .Include(g => g.Categories)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _dbContext.Games
                .Include(g => g.Categories)
                .Include(g => g.Platforms)
                .ToListAsync();
        }

        public async Task<PagedResult<Game>> GetPagedAsync(
            int page,
            int pageSize,
            string? name = null,
            string? genre = null,
            string? platform = null)
        {
            if (page < 1) throw new ArgumentOutOfRangeException(nameof(page));
            if (pageSize < 1) throw new ArgumentOutOfRangeException(nameof(pageSize));

            var query = _dbContext.Games
                                  .Include(g => g.Categories)
                                  .Include(g => g.Platforms)
                                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                var loweredName = name.ToLower();
                query = query.Where(g => g.Name.ToLower().Contains(loweredName));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                var genreList = genre
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(g => g.Trim().ToLower())
                    .ToList();

                query = query.Where(g =>
                    genreList.All(selectedGenre =>
                        g.Categories.Any(c => c.Name.ToLower() == selectedGenre)
                    )
                );
            }

            if (!string.IsNullOrWhiteSpace(platform))
            {
                var platformList = platform
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim().ToLower())
                    .ToList();

                query = query.Where(g =>
                    platformList.All(selectedPlatform =>
                        g.Platforms.Any(p => p.Name.ToLower() == selectedPlatform)
                    )
                );
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(g => g.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Game>
            {
                Items = items,
                TotalCount = total
            };
        }

    }
}
