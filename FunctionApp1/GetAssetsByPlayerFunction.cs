using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;

public class GetAssetsByPlayerFunction
{
    [Function("getassetsbyplayer")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getassetsbyplayer")] HttpRequest req)
    {
        string connectionString =
  "Data Source=PHAMCHIDUC\\SQLEXPRESS;Initial Catalog=BATTLEGAMEe;User ID=sa;Password=phamduc18;TrustServerCertificate=True";

        List<object> result = new List<object>();

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                SELECT 
                    p.PlayerName,
                    p.Level,
                    p.Age,
                    a.AssetName
                FROM Player p
                JOIN PlayerAsset pa ON p.PlayerId = pa.PlayerId
                JOIN Asset a ON pa.AssetId = a.AssetId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new
                        {
                            PlayerName = reader["PlayerName"].ToString(),
                            Level = Convert.ToInt32(reader["Level"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            AssetName = reader["AssetName"].ToString()
                        });
                    }
                }
            }

            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(new
            {
                message = "Error",
                error = ex.Message
            });
        }
    }
}