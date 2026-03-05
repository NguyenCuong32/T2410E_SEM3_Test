using System;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BattleGameAPI
{
    public static class getleaderboard
    {
        [FunctionName("getleaderboard")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Leaderboard requested.");

            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            string result = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT Username, Level, Gold FROM Players ORDER BY Gold DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result += reader["Username"] + " | Level: "
                           + reader["Level"] + " | Gold: "
                           + reader["Gold"] + "\n";
                }
            }

            return new OkObjectResult(result);
        }
    }
}