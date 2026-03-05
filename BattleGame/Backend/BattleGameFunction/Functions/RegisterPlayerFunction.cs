using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using BattleGameFunction.Data;
using BattleGameFunction.Models;

namespace BattleGameFunction.Functions
{
    public class RegisterPlayerFunction
    {
        private readonly BattleGameDbContext _context;

        public RegisterPlayerFunction(BattleGameDbContext context)
        {
            _context = context;
        }

        [Function("registerplayer")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var player = JsonSerializer.Deserialize<Player>(body);

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Player registered successfully");
            return response;
        }
    }
}