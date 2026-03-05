using System.Net;
using BattleGame.Functions.Data;
using BattleGame.Functions.DTOs;
using BattleGame.Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BattleGame.Functions.Functions;

public class AssetFunctions
{
    private readonly BattleGameDbContext _dbContext;
    private readonly ILogger<AssetFunctions> _logger;

    public AssetFunctions(BattleGameDbContext dbContext, ILogger<AssetFunctions> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [Function("createasset")]
    [OpenApiOperation(operationId: "CreateAsset", tags: new[] { "Asset" }, Summary = "Create a new asset")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateAssetRequest), Description = "Asset information", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Asset), Description = "Asset created successfully")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(string), Description = "Invalid request body")]
    public async Task<HttpResponseData> CreateAsset(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "createasset")] HttpRequestData req)
    {
        _logger.LogInformation("CreateAsset triggered.");

        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonConvert.DeserializeObject<CreateAssetRequest>(body);

        if (request == null)
        {
            var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteStringAsync("Invalid request body.");
            return badResponse;
        }

        var asset = new Asset
        {
            AssetName = request.AssetName,
            LevelRequire = request.LevelRequire
        };

        _dbContext.Assets.Add(asset);
        await _dbContext.SaveChangesAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(asset);
        return response;
    }
}
