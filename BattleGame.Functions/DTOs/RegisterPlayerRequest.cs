namespace BattleGame.Functions.DTOs;

public class RegisterPlayerRequest
{
    public string? PlayerName { get; set; }
    public string? FullName { get; set; }
    public string? Age { get; set; }
    public int? Level { get; set; }
    public string? Email { get; set; }
}
