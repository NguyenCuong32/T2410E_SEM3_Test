
using System.IO;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using System.Net;

public class CreateAsset
{
    private readonly string conn = Environment.GetEnvironmentVariable("SqlConnection");

    [Function("createasset")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonSerializer.Deserialize<Asset>(body);

        using SqlConnection connection = new SqlConnection(conn);
        connection.Open();

        var cmd = new SqlCommand("INSERT INTO Asset(AssetName) VALUES(@n)",connection);
        cmd.Parameters.AddWithValue("@n", data.AssetName);
        cmd.ExecuteNonQuery();

        var res = req.CreateResponse(HttpStatusCode.OK);
        await res.WriteStringAsync("Asset created");
        return res;
    }
}

public class Asset
{
    public string AssetName {get;set;}
}
