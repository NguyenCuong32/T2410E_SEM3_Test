using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using T2410E_SEM3_Test.Data;
using T2410E_SEM3_Test.Models;

namespace T2410E_SEM3_Test.Functions
{
    public class RegisterPlayer
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;

        public RegisterPlayer(ILoggerFactory loggerFactory, AppDbContext context)
        {
            _logger = loggerFactory.CreateLogger<RegisterPlayer>();
            _context = context;
        }

        [Function("registerplayer")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Processing registerplayer request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonSerializer.Deserialize<PlayerRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (data == null || string.IsNullOrWhiteSpace(data.PlayerName))
                {
                    var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                    await badResponse.WriteStringAsync("PlayerName is required.");
                    return badResponse;
                }

                var player = new Player
                {
                    PlayerName = data.PlayerName,
                    FullName = data.FullName,
                    Age = data.Age,
                    Level = data.Level ?? 1,
                    Email = data.Email
                };

                _context.Players.Add(player);
                await _context.SaveChangesAsync();

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(player);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in registerplayer: {ex.Message}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("An error occurred while registering the player.");
                return errorResponse;
            }
        }
    }

    public class PlayerRequest
    {
        public string PlayerName { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Age { get; set; }
        public int? Level { get; set; }
        public string? Email { get; set; }
    }
}
