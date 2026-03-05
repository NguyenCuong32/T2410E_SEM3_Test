[Function("createasset")]
public async Task<IActionResult> CreateAsset(
    [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
    ILogger log,
    [FromServices] BattleGameContext db)
{
    var body = await new StreamReader(req.Body).ReadToEndAsync();
    var asset = JsonConvert.DeserializeObject<Asset>(body);

    asset.AssetId = Guid.NewGuid();
    db.Assets.Add(asset);
    await db.SaveChangesAsync();

    return new OkObjectResult("Asset created successfully");
}