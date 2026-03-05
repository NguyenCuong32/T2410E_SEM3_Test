using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using BattleGameSolution.Models;

namespace BattleGameSolution.Functions;

public class RegisterPlayerFunction
{
    [Function("registerplayer")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        Player player = JsonConvert.DeserializeObject<Player>(requestBody);

        string connStr = Environment.GetEnvironmentVariable("SqlConnectionString");

        using SqlConnection conn = new SqlConnection(connStr);

        string query = @"INSERT INTO Player VALUES
        (@PlayerId,@PlayerName,@FullName,@Age,@Level,@Email)";

        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@PlayerId", Guid.NewGuid());
        cmd.Parameters.AddWithValue("@PlayerName", player.PlayerName);
        cmd.Parameters.AddWithValue("@FullName", player.FullName);
        cmd.Parameters.AddWithValue("@Age", player.Age);
        cmd.Parameters.AddWithValue("@Level", player.Level);
        cmd.Parameters.AddWithValue("@Email", player.Email);

        await conn.OpenAsync();
        cmd.ExecuteNonQuery();

        return new OkObjectResult("Player created");
    }
}