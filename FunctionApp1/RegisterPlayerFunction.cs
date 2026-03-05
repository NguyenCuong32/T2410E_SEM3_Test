using System.IO;
using FunctionApp1.Model;
using System.Data.SqlClient;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

public class RegisterPlayerFunction
{
    [Function("registerplayer")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var data = JsonConvert.DeserializeObject<PlayerModel>(requestBody);
        string connectionString =
     "Data Source=PHAMCHIDUC\\SQLEXPRESS;Initial Catalog=BATTLEGAMEe;User ID=sa;Password=phamduc18;TrustServerCertificate=True";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = @"INSERT INTO Player(PlayerName,FullName,Age,Level,Email)
                             VALUES(@PlayerName,@FullName,@Age,@Level,@Email)";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@PlayerName", data.PlayerName);
            cmd.Parameters.AddWithValue("@FullName", data.FullName);
            cmd.Parameters.AddWithValue("@Age", data.Age);
            cmd.Parameters.AddWithValue("@Level", data.Level);
            cmd.Parameters.AddWithValue("@Email", data.Email);

            cmd.ExecuteNonQuery();
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Player created successfully");

        return response;
    }
}