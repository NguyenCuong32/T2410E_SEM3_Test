using ComicSysApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ComicSystemDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ComicSystem")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Serve static files from wwwroot (simple frontend for the Web API)
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

// Fallback to index.html for SPA routes
app.MapFallbackToFile("index.html");

app.Run();
