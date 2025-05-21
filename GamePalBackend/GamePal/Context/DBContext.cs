using GamePal.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamePal.Context
{
    public class DBContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {


        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<GamingPreference> GamingPreferences { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(u => u.GamingPreference)
                .WithOne(gp => gp.User)
                .HasForeignKey<GamingPreference>()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}
