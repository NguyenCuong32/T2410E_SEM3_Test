using System;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FunctionApp1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class CreateAssetFunction
{
    [Function("createasset")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "createasset")] HttpRequest req)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<AssetModel>(requestBody);

            if (data == null || string.IsNullOrEmpty(data.AssetName))
            {
                return new BadRequestObjectResult("Invalid data");
            }
            string connectionString =
         "Data Source=PHAMCHIDUC\\SQLEXPRESS;Initial Catalog=BATTLEGAMEe;User ID=sa;Password=phamduc18;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Asset (AssetName, LevelRequire)
                                 VALUES (@AssetName, @LevelRequire)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AssetName", data.AssetName);
                    cmd.Parameters.AddWithValue("@LevelRequire", data.LevelRequire);

                    cmd.ExecuteNonQuery();
                }
            }

            return new OkObjectResult(new
            {
                message = "Asset created successfully"
            });
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new
            {
                error = ex.Message
            });
        }
    }
}