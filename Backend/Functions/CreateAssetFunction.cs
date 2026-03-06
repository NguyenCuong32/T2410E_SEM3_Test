using System.Net;
using System.Text.Json;
using Backend.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Backend.Functions
{
    public class CreateAssetFunction
    {
        private readonly ILogger _logger;

        public CreateAssetFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CreateAssetFunction>();
        }

        [Function("createasset")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Processing createasset request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var asset = JsonSerializer.Deserialize<Asset>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (asset == null)
                {
                    response = req.CreateResponse(HttpStatusCode.BadRequest);
                    await response.WriteStringAsync("Invalid asset data.");
                    return response;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var sql = "INSERT INTO Asset (AssetId, AssetName, LevelRequire) VALUES (@Id, @Name, @Lvl)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        cmd.Parameters.AddWithValue("@Name", asset.AssetName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Lvl", asset.LevelRequire);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await response.WriteStringAsync("Asset created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating asset.");
                response = req.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync("Error creating asset.");
            }

            return response;
        }
    }
}
