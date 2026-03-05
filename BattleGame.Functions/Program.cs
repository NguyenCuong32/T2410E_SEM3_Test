using BattleGame.Functions.Configurations;
using BattleGame.Functions.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(worker => worker.UseNewtonsoftJson())
    .ConfigureOpenApi()
    .ConfigureServices(services =>
    {
        var connectionString = Environment.GetEnvironmentVariable("MySqlConnection")
            ?? "Server=localhost;Port=3306;Database=BATTLEGAME;User=root;Password=yourpassword;";

        services.AddDbContext<BattleGameDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0))));

        services.AddSingleton<IOpenApiConfigurationOptions, OpenApiConfigurationOptions>();
    })
    .Build();

await host.RunAsync();
