using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BattlegameAPI.Functions;

public class GetAssetsByPlayer
{
    private readonly string _connStr;

    public GetAssetsByPlayer(IConfiguration config)
    {
        _connStr = config["SqlConnectionString"];
    }

    [Function("getassetsbyplayer")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {

        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
SELECT 
    p.PlayerName,
    p.Level,
    p.Age,
    a.AssetName
FROM PlayerAsset pa
JOIN Player p ON pa.PlayerId = p.PlayerId
JOIN Asset a ON pa.AssetId = a.AssetId
";

        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();

        var list = new List<object>();

        while (await reader.ReadAsync())
        {
            list.Add(new
            {
                PlayerName = reader["PlayerName"].ToString(),
                Level = Convert.ToInt32(reader["Level"]),
                Age = Convert.ToInt32(reader["Age"]),
                AssetName = reader["AssetName"].ToString()
            });
        }

        return new OkObjectResult(list);
    }
}