using GamePal.Context;
using GamePal.Repositories.GameRepo;
using GamePal.Repositories.UserGameRepo;
using GamePal.Services.GameServices;
using GamePal.Services.UserGameServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


AddDb(builder);
AddServices(builder);

var app = builder.Build();

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