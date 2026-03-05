using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BattlegameAPI;

public class RemoveAsset
{
    private readonly string _connStr;

    public RemoveAsset(IConfiguration config)
    {
        _connStr = config["SqlConnectionString"];
    }

    [Function("removeasset")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete")] HttpRequest req)
    {
        string playerId = req.Query["playerId"];
        string assetId = req.Query["assetId"];

        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "DELETE FROM PlayerAsset WHERE PlayerId=@p AND AssetId=@a";

        var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@p", playerId);
        cmd.Parameters.AddWithValue("@a", assetId);

        await cmd.ExecuteNonQueryAsync();

        return new OkObjectResult("Removed");
    }
}