using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using T2410E_SEM3_Test.Data;
using T2410E_SEM3_Test.Models;

namespace T2410E_SEM3_Test.Functions
{
    public class CreateAsset
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;

        public CreateAsset(ILoggerFactory loggerFactory, AppDbContext context)
        {
            _logger = loggerFactory.CreateLogger<CreateAsset>();
            _context = context;
        }

        [Function("createasset")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Processing createasset request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonSerializer.Deserialize<AssetRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (data == null || string.IsNullOrWhiteSpace(data.AssetName))
                {
                    var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                    await badResponse.WriteStringAsync("AssetName is required.");
                    return badResponse;
                }

                var asset = new Asset
                {
                    AssetName = data.AssetName,
                    LevelRequire = data.LevelRequire ?? 0
                };

                _context.Assets.Add(asset);
                await _context.SaveChangesAsync();

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(asset);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in createasset: {ex.Message}");
                var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
                await errorResponse.WriteStringAsync("An error occurred while creating the asset.");
                return errorResponse;
            }
        }
    }

    public class AssetRequest
    {
        public string AssetName { get; set; } = null!;
        public int? LevelRequire { get; set; }
    }
}
