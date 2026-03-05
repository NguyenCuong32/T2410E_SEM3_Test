using System.IO;
using System.Threading.Tasks;
using BattleGameFunctionAPI.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Microsoft.Data.SqlClient;

namespace BattleGameFunctionAPI.Functions
{
    public class CreateAssetFunction
    {
        private readonly DatabaseService _databaseService;

        public CreateAssetFunction(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [Function("CreateAsset")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "createasset")] HttpRequestData req)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();

            string assetName = "";
            int levelRequire = 0;

            if (body.Contains("assetName"))
            {
                int start = body.IndexOf("assetName") + 12;
                int end = body.IndexOf("\"", start);
                assetName = body.Substring(start, end - start);
            }

            string query = @"INSERT INTO Asset (AssetName,LevelRequire)
                             VALUES (@AssetName,@LevelRequire)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@AssetName", assetName),
                new SqlParameter("@LevelRequire", levelRequire)
            };

            await _databaseService.ExecuteNonQueryAsync(query, parameters);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Asset created");

            return response;
        }
    }
}