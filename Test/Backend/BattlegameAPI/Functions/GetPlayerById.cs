using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BattlegameAPI;

public class GetPlayerById
{
    private readonly string _connStr;

    public GetPlayerById(IConfiguration config)
    {
        _connStr = config["SqlConnectionString"];
    }

    [Function("playerbyid")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        string id = req.Query["id"];

        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var cmd = new SqlCommand(
            "SELECT * FROM Player WHERE PlayerId=@id", conn);

        cmd.Parameters.AddWithValue("@id", id);

        var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var player = new
            {
                PlayerName = reader["PlayerName"].ToString(),
                FullName = reader["FullName"].ToString(),
                Age = reader["Age"],
                Level = reader["Level"],
                Email = reader["Email"]
            };

            return new OkObjectResult(player);
        }

        return new NotFoundResult();
    }
}