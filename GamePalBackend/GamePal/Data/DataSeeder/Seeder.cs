using GamePal.Context;
using GamePal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Data.DataSeeder
{
    public class Seeder
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        DBContext _dbContext;

        public Seeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DBContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }


        public async Task SeedAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedGameCategoriesAsync();
            await SeedPlatformsAsync();
            await SeedCountriesAsync();
            await SeedLanguagesAsync();
            await SeedGamesAsync();

        }

        private async Task SeedRolesAsync()
        {
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            var users = new[]
            {
            new { UserName = "admin_example", Email = "admin@gmail.com", Password = "Password12345!", Role = "Admin" },
            new { UserName = "user_example", Email = "user@gmail.com", Password = "Password12345!", Role = "User" }
        };

            foreach (var userInfo in users)
            {
                if (await _userManager.FindByEmailAsync(userInfo.Email) == null)
                {
                    var user = new User
                    {
                        UserName = userInfo.UserName,
                        Email = userInfo.Email,
                        EmailConfirmed = false
                    };

                    var result = await _userManager.CreateAsync(user, userInfo.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, userInfo.Role);
                    }
                    else
                    {
                        throw new Exception(
                            $"Failed to create user {userInfo.UserName}: {string.Join(", ", result.Errors)}");
                    }
                }
            }
        }

        private async Task SeedGameCategoriesAsync()
        {
            if (!await _dbContext.GameCategories.AnyAsync())
            {
                var categories = new[]
                {
                new GameCategory { Name = "Adventure" },
                new GameCategory { Name = "FPS" },
                new GameCategory { Name = "Sandbox" },
                new GameCategory { Name = "Strategy" },
                new GameCategory { Name = "Platformer" },
                new GameCategory { Name = "Survival" },
                new GameCategory { Name = "Card" },
                new GameCategory { Name = "Battle Royale" },
                new GameCategory { Name = "Extraction Shooter" }


            };

                _dbContext.GameCategories.AddRange(categories);
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedCountriesAsync()
        {
            if (!await _dbContext.Countries.AnyAsync())
            {
                var countries = new[]
                {
                new Country { Name = "Canada" },
                new Country { Name = "Germany" },
                new Country { Name = "Hungary" },
                new Country { Name = "Slovakia" },
                new Country { Name = "Great Britain" },
                new Country { Name = "Australia" },
                new Country { Name = "Austria" }
            };

                _dbContext.Countries.AddRange(countries);
                await _dbContext.SaveChangesAsync();
            }

        }

        private async Task SeedLanguagesAsync()
        {
            if (!await _dbContext.Languages.AnyAsync())
            {
                var languages = new[]
                {
                new Language { Name = "English" },
                new Language { Name = "German" },
                new Language { Name = "Hungarian" },
                new Language { Name = "Slovak" },
                new Language { Name = "Spanish" },
                new Language { Name = "French" },
                new Language { Name = "Dutch" }
            };

                _dbContext.Languages.AddRange(languages);
                await _dbContext.SaveChangesAsync();
            }
        }


        private async Task SeedPlatformsAsync()
        {
            if (!await _dbContext.Platforms.AnyAsync())
            {
                var platforms = new[]
                {
                new Platform { Name = "PC"},
                new Platform { Name = "PS 4" },
                new Platform { Name = "PS 5" },
                new Platform { Name = "Xbox Series X" },
            };

                _dbContext.Platforms.AddRange(platforms);
                await _dbContext.SaveChangesAsync();
            }
        }


        private async Task SeedGamesAsync()
        {
            if (!await _dbContext.Games.AnyAsync())
            {
                var allCategories = await _dbContext.GameCategories.ToListAsync();
                var byName = allCategories.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);


                var games = new[]
                {
                new Game
                {
                    Name = "PlayerUnknown’s Battlegrounds",
                    ImageUrl = "https://devimages-cdn.apple.com/wwdc-services/articles/images/3D5F5DD3-14F7-4384-94C0-798D15EE7CD7/2048.jpeg",
                    Categories = new List<GameCategory>
                     {
                         byName["Battle Royale"],
                     },
                },
                new Game
                {
                    Name = "Escape From Tarkov",
                    ImageUrl = "https://devimages-cdn.apple.com/wwdc-services/articles/images/3D5F5DD3-14F7-4384-94C0-798D15EE7CD7/2048.jpeg",
                    Categories = new List<GameCategory>
                     {
                       byName["Extraction Shooter"],
                       byName["FPS"]
                     }
                },
                new Game
                {
                    Name = "Chained Together",
                    ImageUrl = "https://devimages-cdn.apple.com/wwdc-services/articles/images/3D5F5DD3-14F7-4384-94C0-798D15EE7CD7/2048.jpeg",
                    Categories = new List<GameCategory>
                     {
                       byName["Platformer"],
                     }

                },
                new Game
                {
                    Name = "Raft",
                    ImageUrl = "https://devimages-cdn.apple.com/wwdc-services/articles/images/3D5F5DD3-14F7-4384-94C0-798D15EE7CD7/2048.jpeg",
                    Categories = new List<GameCategory>
                     {
                       byName["Adventure"],
                       byName["Survival"],
                     }

                },
                new Game
                {
                    Name = "Satisfactory",
                    ImageUrl = "https://devimages-cdn.apple.com/wwdc-services/articles/images/3D5F5DD3-14F7-4384-94C0-798D15EE7CD7/2048.jpeg",
                    Categories = new List<GameCategory>
                     {
                       byName["Sandbox"],
                     }
                },
                new Game
                {
                    Name = "Factorio",
                    ImageUrl = "https://devimages-cdn.apple.com/wwdc-services/articles/images/3D5F5DD3-14F7-4384-94C0-798D15EE7CD7/2048.jpeg",
                    Categories = new List<GameCategory>
                     {
                       byName["Sandbox"],
                     }
                },
                new Game
                {
                    Name = "Europa Universalis IV",
                    ImageUrl = "https://devimages-cdn.apple.com/wwdc-services/articles/images/3D5F5DD3-14F7-4384-94C0-798D15EE7CD7/2048.jpeg",
                    Categories = new List<GameCategory>
                     {
                       byName["Strategy"],
                     }

                }
            };

                _dbContext.Games.AddRange(games);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
