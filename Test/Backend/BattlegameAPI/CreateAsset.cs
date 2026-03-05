using BattlegameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace BattlegameAPI;

public class CreateAsset
{
    private readonly string _connStr;

    public CreateAsset(IConfiguration config)
    {
        _connStr = config["SqlConnectionString"]!;
    }

    [Function("createasset")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();

        var asset = JsonSerializer.Deserialize<Asset>(body,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (asset is null)
            return new BadRequestObjectResult("Invalid JSON");

        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = @"
INSERT INTO Asset(AssetName, LevelRequire)
VALUES (@name, @level);
";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@name", asset.AssetName);
        cmd.Parameters.AddWithValue("@level", asset.LevelRequire);

        await cmd.ExecuteNonQueryAsync();

        return new OkObjectResult("Asset inserted");
    }
}