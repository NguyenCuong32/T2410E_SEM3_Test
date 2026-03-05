using System.Net;
using BattleGame.Functions.Data;
using BattleGame.Functions.DTOs;
using BattleGame.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BattleGame.Functions.Functions;

public class PlayerFunctions
{
    private readonly BattleGameDbContext _dbContext;
    private readonly ILogger<PlayerFunctions> _logger;

    public PlayerFunctions(BattleGameDbContext dbContext, ILogger<PlayerFunctions> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [Function("registerplayer")]
    [OpenApiOperation(operationId: "RegisterPlayer", tags: new[] { "Player" }, Summary = "Register a new player")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(RegisterPlayerRequest), Description = "Player registration information", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Player), Description = "Player registered successfully")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid request body")]
    public async Task<HttpResponseData> RegisterPlayer(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "registerplayer")] HttpRequestData req)
    {
        _logger.LogInformation("RegisterPlayer triggered.");

        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonConvert.DeserializeObject<RegisterPlayerRequest>(body);

        if (request == null)
        {
            var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteStringAsync("Invalid request body.");
            return badResponse;
        }

        var player = new Player
        {
            PlayerName = request.PlayerName,
            FullName = request.FullName,
            Age = request.Age,
            Level = request.Level,
            Email = request.Email
        };

        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(player);
        return response;
    }

    [Function("getassetsbyplayer")]
    [OpenApiOperation(operationId: "GetAssetsByPlayer", tags: new[] { "Player" }, Summary = "Get all assets of all players (report)")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<PlayerAssetResponse>), Description = "Report of all players with their assets")]
    public async Task<HttpResponseData> GetAssetsByPlayer(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getassetsbyplayer")] HttpRequestData req)
    {
        _logger.LogInformation("GetAssetsByPlayer (all players) triggered.");

        var results = await _dbContext.PlayerAssets
            .Include(pa => pa.Player)
            .Include(pa => pa.Asset)
            .OrderBy(pa => pa.Player!.PlayerName)
            .Select(pa => new PlayerAssetResponse
            {
                PlayerName = pa.Player!.PlayerName,
                Level = pa.Player.Level,
                Age = pa.Player.Age,
                AssetName = pa.Asset!.AssetName
            })
            .ToListAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(results);
        return response;
    }

    [Function("getassetsbyplayerid")]
    [OpenApiOperation(operationId: "GetAssetsByPlayerId", tags: new[] { "Player" }, Summary = "Get assets of a specific player")]
    [OpenApiParameter(name: "playerId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The unique identifier of the player")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<PlayerAssetResponse>), Description = "Assets owned by the player")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(string), Description = "Player not found")]
    public async Task<HttpResponseData> GetAssetsByPlayerId(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getassetsbyplayer/{playerId:guid}")] HttpRequestData req,
        Guid playerId)
    {
        _logger.LogInformation("GetAssetsByPlayerId triggered for PlayerId: {PlayerId}", playerId);

        var playerExists = await _dbContext.Players.AnyAsync(p => p.PlayerId == playerId);
        if (!playerExists)
        {
            var notFound = req.CreateResponse(HttpStatusCode.NotFound);
            await notFound.WriteStringAsync($"Player with ID {playerId} not found.");
            return notFound;
        }

        var assets = await _dbContext.PlayerAssets
            .Include(pa => pa.Player)
            .Include(pa => pa.Asset)
            .Where(pa => pa.PlayerId == playerId)
            .Select(pa => new PlayerAssetResponse
            {
                PlayerName = pa.Player!.PlayerName,
                Level = pa.Player.Level,
                Age = pa.Player.Age,
                AssetName = pa.Asset!.AssetName
            })
            .ToListAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(assets);
        return response;
    }
}
