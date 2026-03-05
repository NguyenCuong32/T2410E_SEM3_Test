using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BattlegameAPI;

public class GetPlayers
{
    private readonly string _connStr;

    public GetPlayers(IConfiguration config)
    {
        _connStr = config["SqlConnectionString"];
    }

    [Function("players")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var cmd = new SqlCommand("SELECT * FROM Player", conn);
        var reader = await cmd.ExecuteReaderAsync();

        var list = new List<object>();

        while (await reader.ReadAsync())
        {
            list.Add(new
            {
                PlayerId = reader["PlayerId"].ToString(),
                PlayerName = reader["PlayerName"].ToString(),
                FullName = reader["FullName"].ToString(),
                Age = reader["Age"],
                Level = reader["Level"],
                Email = reader["Email"]
            });
        }

        return new OkObjectResult(list);
    }
}