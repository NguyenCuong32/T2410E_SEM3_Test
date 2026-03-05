using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace BattlegameAPI;

public class AssignAsset
{
    private readonly string _connStr;

    public AssignAsset(IConfiguration config)
    {
        _connStr = config["SqlConnectionString"];
    }

    [Function("assignasset")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();

        var data = JsonSerializer.Deserialize<AssignRequest>(body,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "INSERT INTO PlayerAsset(PlayerId,AssetId) VALUES (@p,@a)";

        var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@p", data.PlayerId);
        cmd.Parameters.AddWithValue("@a", data.AssetId);

        await cmd.ExecuteNonQueryAsync();

        return new OkObjectResult("Assigned");
    }
}

public class AssignRequest
{
    public string PlayerId { get; set; }
    public string AssetId { get; set; }
}