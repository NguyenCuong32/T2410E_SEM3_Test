using System.Net;
using System.Text.Json;
using Backend.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Backend.Functions
{
    public class RegisterPlayerFunction
    {
        private readonly ILogger _logger;

        public RegisterPlayerFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RegisterPlayerFunction>();
        }

        [Function("registerplayer")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Processing registerplayer request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var player = JsonSerializer.Deserialize<Player>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (player == null)
                {
                    response = req.CreateResponse(HttpStatusCode.BadRequest);
                    await response.WriteStringAsync("Invalid player data.");
                    return response;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var sql = "INSERT INTO Player (PlayerId, PlayerName, FullName, Age, [Level], Email) VALUES (@Id, @Name, @Full, @Age, @Lvl, @Email)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        cmd.Parameters.AddWithValue("@Name", player.PlayerName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Full", player.FullName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Age", player.Age ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Lvl", player.Level);
                        cmd.Parameters.AddWithValue("@Email", player.Email ?? (object)DBNull.Value);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await response.WriteStringAsync("Player registered successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering player.");
                response = req.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync("Error registering player.");
            }

            return response;
        }
    }
}
