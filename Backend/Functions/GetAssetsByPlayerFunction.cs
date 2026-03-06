using System.Net;
using System.Text.Json;
using Backend.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Backend.Functions
{
    public class GetAssetsByPlayerFunction
    {
        private readonly ILogger _logger;

        public GetAssetsByPlayerFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetAssetsByPlayerFunction>();
        }

        [Function("getassets")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("Processing getassets request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            var reports = new List<PlayerAssetReport>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var sql = @"
                        SELECT 
                            ROW_NUMBER() OVER (ORDER BY p.PlayerName) AS [No],
                            p.PlayerName, 
                            p.[Level], 
                            p.Age, 
                            a.AssetName
                        FROM Player p
                        JOIN PlayerAsset pa ON p.PlayerId = pa.PlayerId
                        JOIN Asset a ON pa.AssetId = a.AssetId";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                reports.Add(new PlayerAssetReport
                                {
                                    No = reader.GetInt64(0) != null ? (int)reader.GetInt64(0) : 0,
                                    PlayerName = reader.GetString(1),
                                    Level = reader.GetInt32(2),
                                    Age = reader.GetString(3),
                                    AssetName = reader.GetString(4)
                                });
                            }
                        }
                    }
                }

                await response.WriteAsJsonAsync(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving asset report.");
                response = req.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync("Error retrieving asset report.");
            }

            return response;
        }
    }
}
