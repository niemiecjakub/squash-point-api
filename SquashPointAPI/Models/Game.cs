namespace SquashPointAPI.Models;

public class Game
{
    public int Id { get; set; }
    public Player Winner;
    public League League { get; set; }
    public ICollection<PlayerGame> PlayerGames { get; set; }
    public ICollection<Set> Sets {get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}