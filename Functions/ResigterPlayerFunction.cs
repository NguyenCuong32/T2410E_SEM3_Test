[Function("registerplayer")]
public async Task<IActionResult> RegisterPlayer(
    [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
    ILogger log,
    [FromServices] BattleGameContext db)
{
    var body = await new StreamReader(req.Body).ReadToEndAsync();
    var player = JsonConvert.DeserializeObject<Player>(body);

    player.PlayerId = Guid.NewGuid();
    db.Players.Add(player);
    await db.SaveChangesAsync();

    return new OkObjectResult("Player registered successfully");
}