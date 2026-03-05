using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using BattleGameSolution.Models;

namespace BattleGameSolution.Functions;

public class CreateAssetFunction
{
    [Function("createasset")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        string body = await new StreamReader(req.Body).ReadToEndAsync();
        Asset asset = JsonConvert.DeserializeObject<Asset>(body);

        string connStr = Environment.GetEnvironmentVariable("SqlConnectionString");

        using SqlConnection conn = new SqlConnection(connStr);

        string query = @"INSERT INTO Asset VALUES
        (@AssetId,@AssetName,@LevelRequire)";

        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@AssetId", Guid.NewGuid());
        cmd.Parameters.AddWithValue("@AssetName", asset.AssetName);
        cmd.Parameters.AddWithValue("@LevelRequire", asset.LevelRequire);

        await conn.OpenAsync();
        cmd.ExecuteNonQuery();

        return new OkObjectResult("Asset created");
    }
}