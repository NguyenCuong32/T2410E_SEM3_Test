using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using T2410E_SEM3_Test.Data;

namespace T2410E_SEM3_Test.Functions
{
    public class GetAssetsByPlayer
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;

        public GetAssetsByPlayer(ILoggerFactory loggerFactory, AppDbContext context)
        {
            _logger = loggerFactory.CreateLogger<GetAssetsByPlayer>();
            _context = context;
        }

        [Function("getassetsbyplayer")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("Processing getassetsbyplayer request.");

            try
            {
                var result = await _context.PlayerAssets
                    .Include(pa => pa.Player)
                    .Include(pa => pa.Asset)
                    .Select(pa => new
                    {
                        PlayerName = pa.Player.PlayerName,
                        Level = pa.Player.Level,
                        Age = pa.Player.Age,
                        AssetName = pa.Asset.AssetName
                    })
                    .OrderBy(r => r.PlayerName)
                    .ToListAsync();

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getassetsbyplayer: {ex.Message}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("An error occurred while fetching player assets.");
                return errorResponse;
            }
        }
    }
}
