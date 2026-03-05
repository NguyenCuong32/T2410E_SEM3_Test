using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BattlegameAPI;

public class GetAssets
{
    private readonly string _connStr;

    public GetAssets(IConfiguration config)
    {
        _connStr = config["SqlConnectionString"];
    }

    [Function("assets")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var cmd = new SqlCommand("SELECT * FROM Asset", conn);
        var reader = await cmd.ExecuteReaderAsync();

        var list = new List<object>();

        while (await reader.ReadAsync())
        {
            list.Add(new
            {
                AssetId = reader["AssetId"],
                AssetName = reader["AssetName"],
                LevelRequire = reader["LevelRequire"]
            });
        }

        return new OkObjectResult(list);
    }
}