[Function("registerplayer")]
public async Task<IActionResult> RegisterPlayer(
    [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
    ILogger log)
{
    var body = await new StreamReader(req.Body).ReadToEndAsync();
    var player = JsonConvert.DeserializeObject<Player>(body);

    using (var db = new BattleGameContext())
    {
        db.Players.Add(player);
        await db.SaveChangesAsync();
    }

    return new OkObjectResult("Player registered successfully");
}