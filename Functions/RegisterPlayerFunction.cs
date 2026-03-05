using System.IO;
using System.Threading.Tasks;
using BattleGameFunctionAPI.Models;
using BattleGameFunctionAPI.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Microsoft.Data.SqlClient;

namespace BattleGameFunctionAPI.Functions
{
    public class RegisterPlayerFunction
    {
        private readonly DatabaseService _databaseService;

        public RegisterPlayerFunction(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [Function("RegisterPlayer")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "registerplayer")] HttpRequestData req)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();

            string playerName = "";
            string fullName = "";
            string age = "";
            int level = 0;
            string email = "";

            if (body.Contains("playerName"))
            {
                int start = body.IndexOf("playerName") + 13;
                int end = body.IndexOf("\"", start);
                playerName = body.Substring(start, end - start);
            }

            string query = @"INSERT INTO Player (PlayerName,FullName,Age,Level,Email)
                             VALUES (@PlayerName,@FullName,@Age,@Level,@Email)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@PlayerName", playerName),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Age", age),
                new SqlParameter("@Level", level),
                new SqlParameter("@Email", email)
            };

            await _databaseService.ExecuteNonQueryAsync(query, parameters);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Player created");

            return response;
        }
    }
}