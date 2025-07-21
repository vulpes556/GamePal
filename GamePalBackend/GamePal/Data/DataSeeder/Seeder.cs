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
                var catByName = allCategories.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);
                var allPlatforms = await _dbContext.Platforms.ToListAsync();
                var platformByName = allPlatforms.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);


                string imageUrl = "https://devimages-cdn.apple.com/wwdc-services/articles/images/3D5F5DD3-14F7-4384-94C0-798D15EE7CD7/2048.jpeg";

                List<GameCategory> catList(params string[] names) => names.Select(n => catByName[n]).ToList();
                List<Platform> platformList(params string[] names) => names.Select(n => platformByName[n]).ToList();

                var games = new[]
                {
                new Game { Name = "PlayerUnknown’s Battlegrounds", ImageUrl = imageUrl, Categories = catList("Battle Royale"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Escape From Tarkov", ImageUrl = imageUrl, Categories = catList("Extraction Shooter", "FPS"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Chained Together", ImageUrl = imageUrl, Categories = catList("Platformer"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Raft", ImageUrl = imageUrl, Categories = catList("Adventure", "Survival"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Satisfactory", ImageUrl = imageUrl, Categories = catList("Sandbox"), Platforms = platformList("PC", "Xbox Series X") },
                new Game { Name = "Factorio", ImageUrl = imageUrl, Categories = catList("Sandbox"), Platforms = platformList("PS 5") },
                new Game { Name = "Europa Universalis IV", ImageUrl = imageUrl, Categories = catList("Strategy"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Minecraft", ImageUrl = imageUrl, Categories = catList("Sandbox", "Survival"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Call of Duty: Modern Warfare II", ImageUrl = imageUrl, Categories = catList("FPS"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Apex Legends", ImageUrl = imageUrl, Categories = catList("Battle Royale", "FPS"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Rust", ImageUrl = imageUrl, Categories = catList("Survival", "FPS"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "The Forest", ImageUrl = imageUrl, Categories = catList("Survival", "Adventure"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Fortnite", ImageUrl = imageUrl, Categories = catList("Battle Royale", "Sandbox"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Valheim", ImageUrl = imageUrl, Categories = catList("Survival", "Adventure"), Platforms = platformList("PC") },
                new Game { Name = "Deep Rock Galactic", ImageUrl = imageUrl, Categories = catList("FPS", "Adventure"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Grounded", ImageUrl = imageUrl, Categories = catList("Survival", "Adventure"), Platforms = platformList("PC", "Xbox Series X") },
                new Game { Name = "Overcooked! 2", ImageUrl = imageUrl, Categories = catList("Platformer"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Phasmophobia", ImageUrl = imageUrl, Categories = catList("Survival", "Adventure"), Platforms = platformList("PC") },
                new Game { Name = "Don't Starve Together", ImageUrl = imageUrl, Categories = catList("Survival", "Sandbox"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Terraria", ImageUrl = imageUrl, Categories = catList("Sandbox", "Adventure"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "ARK: Survival Evolved", ImageUrl = imageUrl, Categories = catList("Survival", "Adventure"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Halo Infinite", ImageUrl = imageUrl, Categories = catList("FPS"), Platforms = platformList("PC", "Xbox Series X") },
                new Game { Name = "Gears 5", ImageUrl = imageUrl, Categories = catList("FPS", "Adventure"), Platforms = platformList("PC", "Xbox Series X") },

                new Game { Name = "Warframe", ImageUrl = imageUrl, Categories = catList("FPS", "Adventure"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Killing Floor 2", ImageUrl = imageUrl, Categories = catList("FPS", "Survival"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Left 4 Dead 2", ImageUrl = imageUrl, Categories = catList("FPS", "Survival"), Platforms = platformList("PC") },
                new Game { Name = "The Division 2", ImageUrl = imageUrl, Categories = catList("FPS", "Adventure"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Borderlands 3", ImageUrl = imageUrl, Categories = catList("FPS", "Sandbox"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Payday 2", ImageUrl = imageUrl, Categories = catList("FPS"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Dying Light", ImageUrl = imageUrl, Categories = catList("Survival", "Adventure"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "7 Days to Die", ImageUrl = imageUrl, Categories = catList("Survival", "Sandbox"), Platforms = platformList("PC") },
                new Game { Name = "No Man's Sky", ImageUrl = imageUrl, Categories = catList("Adventure", "Survival"), Platforms = platformList("PC", "PS 5") },

                new Game { Name = "Hunt: Showdown", ImageUrl = imageUrl, Categories = catList("FPS", "Extraction Shooter"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Back 4 Blood", ImageUrl = imageUrl, Categories = catList("FPS", "Survival"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Hell Let Loose", ImageUrl = imageUrl, Categories = catList("FPS", "Strategy"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Squad", ImageUrl = imageUrl, Categories = catList("FPS", "Strategy"), Platforms = platformList("PC") },
                new Game { Name = "Starbound", ImageUrl = imageUrl, Categories = catList("Sandbox", "Survival"), Platforms = platformList("PC") },
                new Game { Name = "V Rising", ImageUrl = imageUrl, Categories = catList("Survival", "Adventure"), Platforms = platformList("PC") },
                new Game { Name = "The Cycle: Frontier", ImageUrl = imageUrl, Categories = catList("Extraction Shooter"), Platforms = platformList("PC") },
                new Game { Name = "Among Us", ImageUrl = imageUrl, Categories = catList("Strategy"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Project Zomboid", ImageUrl = imageUrl, Categories = catList("Survival", "Strategy"), Platforms = platformList("PC") },
                new Game { Name = "Enlisted", ImageUrl = imageUrl, Categories = catList("FPS", "Strategy"), Platforms = platformList("PC", "PS 5") },

                new Game { Name = "Fall Guys", ImageUrl = imageUrl, Categories = catList("Platformer"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Human: Fall Flat", ImageUrl = imageUrl, Categories = catList("Platformer", "Sandbox"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Totally Accurate Battlegrounds", ImageUrl = imageUrl, Categories = catList("Battle Royale"), Platforms = platformList("PC") },
                new Game { Name = "CRSED: F.O.A.D.", ImageUrl = imageUrl, Categories = catList("Battle Royale", "FPS"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Z1 Battle Royale", ImageUrl = imageUrl, Categories = catList("Battle Royale"), Platforms = platformList("PC") },
                new Game { Name = "Super Animal Royale", ImageUrl = imageUrl, Categories = catList("Battle Royale"), Platforms = platformList("PC", "Xbox Series X") },
                new Game { Name = "Rogue Company", ImageUrl = imageUrl, Categories = catList("FPS"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Warface", ImageUrl = imageUrl, Categories = catList("FPS"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "CrossfireX", ImageUrl = imageUrl, Categories = catList("FPS"), Platforms = platformList("Xbox Series X") },
                new Game { Name = "Operation: Harsh Doorstop", ImageUrl = imageUrl, Categories = catList("FPS", "Strategy"), Platforms = platformList("PC") },

                new Game { Name = "Unturned", ImageUrl = imageUrl, Categories = catList("Survival"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Eco", ImageUrl = imageUrl, Categories = catList("Strategy", "Sandbox"), Platforms = platformList("PC") },
                new Game { Name = "The Wild Eight", ImageUrl = imageUrl, Categories = catList("Survival"), Platforms = platformList("PC") },
                new Game { Name = "Risk of Rain 2", ImageUrl = imageUrl, Categories = catList("Platformer", "Adventure"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Move or Die", ImageUrl = imageUrl, Categories = catList("Platformer"), Platforms = platformList("PC") },
                new Game { Name = "SpeedRunners", ImageUrl = imageUrl, Categories = catList("Platformer"), Platforms = platformList("PC") },
                new Game { Name = "Tribes of Midgard", ImageUrl = imageUrl, Categories = catList("Survival", "Adventure"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "Citadel: Forged with Fire", ImageUrl = imageUrl, Categories = catList("Survival", "Sandbox"), Platforms = platformList("PC", "PS 4") },
                new Game { Name = "Myth of Empires", ImageUrl = imageUrl, Categories = catList("Survival", "Strategy"), Platforms = platformList("PC") },
                new Game { Name = "Survive the Nights", ImageUrl = imageUrl, Categories = catList("Survival"), Platforms = platformList("PC") },
                new Game { Name = "Helldivers 2", ImageUrl = imageUrl, Categories = catList("FPS", "Sandbox"), Platforms = platformList("PC", "PS 5") },
                new Game { Name = "The Finals", ImageUrl = imageUrl, Categories = catList("FPS"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "It Takes Two", ImageUrl = imageUrl, Categories = catList("Platformer", "Adventure"), Platforms = platformList("PC", "PS 4", "PS 5", "Xbox Series X") },
                new Game { Name = "Forza Horizon 5", ImageUrl = imageUrl, Categories = catList("Sandbox", "Adventure"), Platforms = platformList("PC", "Xbox Series X") },
                new Game { Name = "Destiny 2", ImageUrl = imageUrl, Categories = catList("FPS"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Monster Hunter Wilds", ImageUrl = imageUrl, Categories = catList("Adventure", "Survival"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Portal 2", ImageUrl = imageUrl, Categories = catList("Platformer", "Strategy"), Platforms = platformList("PC", "PS 4", "PS 5", "Xbox Series X") },
                new Game { Name = "Stardew Valley", ImageUrl = imageUrl, Categories = catList("Sandbox", "Survival"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Sea of Thieves", ImageUrl = imageUrl, Categories = catList("Adventure", "Survival"), Platforms = platformList("PC", "Xbox Series X") },
                new Game { Name = "Rocket League", ImageUrl = imageUrl, Categories = catList("Strategy"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Overwatch 2", ImageUrl = imageUrl, Categories = catList("FPS", "Strategy"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
                new Game { Name = "Rainbow Six Extraction", ImageUrl = imageUrl, Categories = catList("FPS", "Strategy"), Platforms = platformList("PC", "PS 5", "Xbox Series X") },
            };

                _dbContext.Games.AddRange(games);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
