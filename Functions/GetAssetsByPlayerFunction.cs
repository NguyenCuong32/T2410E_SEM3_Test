using System.Threading.Tasks;
using BattleGameFunctionAPI.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Data;

namespace BattleGameFunctionAPI.Functions
{
    public class GetAssetsByPlayerFunction
    {
        private readonly DatabaseService _databaseService;

        public GetAssetsByPlayerFunction(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [Function("GetAssetsByPlayer")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getassets")] HttpRequestData req)
        {
            string query = @"
            SELECT 
            p.PlayerName,
            p.Level,
            p.Age,
            a.AssetName
            FROM Player p
            JOIN PlayerAsset pa ON p.PlayerId = pa.PlayerId
            JOIN Asset a ON pa.AssetId = a.AssetId";

            DataTable table = await _databaseService.ExecuteQueryAsync(query);

            var response = req.CreateResponse(HttpStatusCode.OK);

            string result = "";

            foreach (DataRow row in table.Rows)
            {
                result += row["PlayerName"] + " - " +
                          row["AssetName"] + "\n";
            }

            await response.WriteStringAsync(result);

            return response;
        }
    }
}