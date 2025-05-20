using GamePal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Context
{
    public class DBContext(DbContextOptions options) : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<GamingPreference> GamingPreferences { get; set; }


    }
}
