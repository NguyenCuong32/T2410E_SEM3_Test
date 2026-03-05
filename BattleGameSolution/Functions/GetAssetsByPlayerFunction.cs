using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BattleGameSolution.Functions;

public class GetAssetsByPlayerFunction
{
    [Function("getassetsbyplayer")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        string playerId = req.Query["playerId"];

        string connStr = Environment.GetEnvironmentVariable("SqlConnectionString");

        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Assets WHERE PlayerId=@playerId",
                conn);

            cmd.Parameters.AddWithValue("@playerId", playerId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }

        List<object> assets = new List<object>();

        foreach (DataRow row in dt.Rows)
        {
            assets.Add(new
            {
                AssetId = row["AssetId"],
                AssetName = row["AssetName"],
                PlayerId = row["PlayerId"]
            });
        }

        return new OkObjectResult(assets);
    }
}