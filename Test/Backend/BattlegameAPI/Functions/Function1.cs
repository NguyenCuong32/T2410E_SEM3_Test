using BattlegameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace BattlegameAPI.Functions;

public class RegisterPlayer
{
    private readonly string _connStr;

    public RegisterPlayer(IConfiguration config)
    {
        _connStr = config["SqlConnectionString"]!;
    }

    [Function("registerplayer")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();

        var player = JsonSerializer.Deserialize<Player>(body,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (player is null)
            return new BadRequestObjectResult("Invalid JSON");

        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
INSERT INTO Player(PlayerName, FullName, Age, Level, Email)
VALUES (@PlayerName, @FullName, @Age, @Level, @Email);
";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PlayerName", player.PlayerName);
        cmd.Parameters.AddWithValue("@FullName", player.FullName);
        cmd.Parameters.AddWithValue("@Age", player.Age);
        cmd.Parameters.AddWithValue("@Level", player.Level);
        cmd.Parameters.AddWithValue("@Email", (object?)player.Email ?? DBNull.Value);

        await cmd.ExecuteNonQueryAsync();

        return new OkObjectResult("Player inserted");
    }
}