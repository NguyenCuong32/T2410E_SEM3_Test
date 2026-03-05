using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using BattleGameFunction.Data;
using BattleGameFunction.Models;

namespace BattleGameFunction.Functions
{
    public class CreateAssetFunction
    {
        private readonly BattleGameDbContext _context;

        public CreateAssetFunction(BattleGameDbContext context)
        {
            _context = context;
        }

        [Function("createasset")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var asset = JsonSerializer.Deserialize<Asset>(body);

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Asset created successfully");
            return response;
        }
    }
}