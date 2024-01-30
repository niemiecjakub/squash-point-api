namespace SquashPointAPI.Models;

public class Game
{
    public int Id { get; set; }
    public ICollection<PlayerGame> PlayerGames { get; set; }
    public int Score { get; set; }
    public League League { get; set; }
}