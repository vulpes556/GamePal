using GamePal.Context;
using GamePal.Data.DataSeeder;
using GamePal.Data.Entities;
using GamePal.Repositories.AuthProviderRepo;
using GamePal.Repositories.GameRepo;
using GamePal.Repositories.UserAuthProviders;
using GamePal.Repositories.UserGameRepo;
using GamePal.Services.GameServices;
using GamePal.Services.UserGameServices;
using GamePal.Services.UserServices;
using LadleMeThis.Services.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


AddDb(builder);
AddServices(builder);
AddIdentityServices(builder);
AddAuthentication(builder);

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var db = scope.ServiceProvider.GetRequiredService<DBContext>();

    var seeder = new Seeder(userManager, roleManager, db);


    if (db.Database.IsRelational())
    {
        //db.Database.Migrate();

        await seeder.SeedAsync();
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


void AddDb(WebApplicationBuilder builder)
{


    builder.Services.AddDbContext<DBContext>(options =>
    {
        options.UseNpgsql(builder.Configuration["DbConnectionString"]);
    });
}


void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IGameService, GameService>();
    builder.Services.AddScoped<IGameRepo, GameRepo>();
    builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();
    builder.Services.AddScoped<IUserGameService, UserGameService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserAuthProviderRepo, UserAuthProviderRepo>();
    builder.Services.AddScoped<IAuthProviderRepo, AuthProviderRepo>();
}

void AddIdentityServices(WebApplicationBuilder builder)
{
    builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DBContext>();

}

void AddAuthentication(WebApplicationBuilder builder)
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // Prefer Authorization header
                    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                    {
                        context.Token = authHeader.Substring("Bearer ".Length).Trim();
                    }
                    else
                    {
                        // Fallback to cookie
                        var cookieToken = context.Request.Cookies["AuthToken"];
                        if (!string.IsNullOrEmpty(cookieToken))
                            context.Token = cookieToken;
                    }

                    return Task.CompletedTask;
                }
            };

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["ValidIssuer"],
                ValidAudience = builder.Configuration["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["IssuerSigningKey"])
                ),
            };
        });
}