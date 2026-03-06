using System.Net;
using System.Text.Json;
using Backend.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Backend.Functions
{
    public class AssignAssetFunction
    {
        private readonly ILogger _logger;

        public AssignAssetFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AssignAssetFunction>();
        }

        [Function("assignasset")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Processing assignasset request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var playerAsset = JsonSerializer.Deserialize<PlayerAsset>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (playerAsset == null || playerAsset.PlayerId == Guid.Empty || playerAsset.AssetId == Guid.Empty)
                {
                    response = req.CreateResponse(HttpStatusCode.BadRequest);
                    await response.WriteStringAsync("Invalid player-asset linkage data.");
                    return response;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var sql = "INSERT INTO PlayerAsset (PlayerId, AssetId) VALUES (@PId, @AId)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@PId", playerAsset.PlayerId);
                        cmd.Parameters.AddWithValue("@AId", playerAsset.AssetId);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await response.WriteStringAsync("Asset assigned to player successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning asset.");
                response = req.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync("Error assigning asset.");
            }

            return response;
        }
    }
}
