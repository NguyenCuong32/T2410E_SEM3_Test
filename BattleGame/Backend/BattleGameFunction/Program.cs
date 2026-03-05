using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;              
using BattleGameFunction.Data;                    

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();


builder.Services.AddDbContext<BattleGameDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=BATTLEGAME;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;"));

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();