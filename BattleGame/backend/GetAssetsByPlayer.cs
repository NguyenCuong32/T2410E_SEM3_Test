
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

public class GetAssetsByPlayer
{
    private readonly string conn = Environment.GetEnvironmentVariable("SqlConnection");

    [Function("getassetsbyplayer")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        var list = new List<object>();

        using SqlConnection connection = new SqlConnection(conn);
        connection.Open();

        string q = @"SELECT p.PlayerName,p.Level,p.Age,a.AssetName
                     FROM PlayerAsset pa
                     JOIN Player p ON pa.PlayerId=p.Id
                     JOIN Asset a ON pa.AssetId=a.Id";

        var cmd = new SqlCommand(q,connection);
        var reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            list.Add(new {
                PlayerName = reader["PlayerName"],
                Level = reader["Level"],
                Age = reader["Age"],
                AssetName = reader["AssetName"]
            });
        }

        var res = req.CreateResponse(HttpStatusCode.OK);
        await res.WriteStringAsync(JsonSerializer.Serialize(list));
        return res;
    }
}
