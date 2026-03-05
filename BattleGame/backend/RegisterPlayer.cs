
using System.IO;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using System.Net;

public class RegisterPlayer
{
    private readonly string conn = Environment.GetEnvironmentVariable("SqlConnection");

    [Function("registerplayer")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonSerializer.Deserialize<Player>(body);

        using SqlConnection connection = new SqlConnection(conn);
        connection.Open();

        var query = "INSERT INTO Player(PlayerName,FullName,Age,Level) VALUES(@p,@f,@a,@l)";
        var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@p", data.PlayerName);
        cmd.Parameters.AddWithValue("@f", data.FullName);
        cmd.Parameters.AddWithValue("@a", data.Age);
        cmd.Parameters.AddWithValue("@l", data.Level);

        cmd.ExecuteNonQuery();

        var res = req.CreateResponse(HttpStatusCode.OK);
        await res.WriteStringAsync("Player registered");
        return res;
    }
}

public class Player
{
    public string PlayerName {get;set;}
    public string FullName {get;set;}
    public int Age {get;set;}
    public int Level {get;set;}
}
