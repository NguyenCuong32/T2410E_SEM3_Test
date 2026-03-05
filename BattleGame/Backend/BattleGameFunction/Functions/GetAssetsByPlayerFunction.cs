using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using BattleGameFunction.Data;

namespace BattleGameFunction.Functions
{
    public class GetAssetsByPlayerFunction
    {
        private readonly BattleGameDbContext _context;

        public GetAssetsByPlayerFunction(BattleGameDbContext context)
        {
            _context = context;
        }

        [Function("getassetsbyplayer")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var data = await _context.PlayerAssets
                .Include(pa => pa.Player)
                .Include(pa => pa.Asset)
                .Select((pa, index) => new
                {
                    No = index + 1,
                    PlayerName = pa.Player.PlayerName,
                    Level = pa.Player.Level,
                    Age = pa.Player.Age,
                    AssetName = pa.Asset.AssetName
                })
                .ToListAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(data);

            return response;
        }
    }
}