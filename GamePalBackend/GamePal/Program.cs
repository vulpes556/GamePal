using GamePal.Context;
using GamePal.Data.Entities;
using GamePal.Repositories.GameRepo;
using GamePal.Repositories.UserGameRepo;
using GamePal.Services.GameServices;
using GamePal.Services.UserGameServices;
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

var app = builder.Build();

//temporary sample user
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    string email = "test@gmail.com";
    string password = "Test1234!";

    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
    {
        var newUser = new User
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            Country = new Country
            {
                Name = "Germany"
            }
        };

        var result = await userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to create sample user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
                    var token = context.Request.Cookies["AuthToken"];
                    if (!string.IsNullOrEmpty(token))
                        context.Token = token;

                    return Task.CompletedTask;
                }
            };

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
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