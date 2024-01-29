namespace SquashPointAPI.Models;

public class Game
{
    public int Id { get; set; }
    public ICollection<Player> Players { get; set; }
    public Player Winner { get; set; }
    public int Score { get; set; }
    public League League { get; set; }
}