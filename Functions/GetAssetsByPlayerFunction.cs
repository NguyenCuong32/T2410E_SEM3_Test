[Function("getassetsbyplayer")]
public async Task<IActionResult> GetAssetsByPlayer(
    [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req,
    ILogger log,
    [FromServices] BattleGameContext db)
{
    var result = from p in db.Players
                 join pa in db.PlayerAssets on p.PlayerId equals pa.PlayerId
                 join a in db.Assets on pa.AssetId equals a.AssetId
                 select new {
                     p.PlayerName,
                     p.Level,
                     p.Age,
                     a.AssetName
                 };

    return new OkObjectResult(result.ToList());
}