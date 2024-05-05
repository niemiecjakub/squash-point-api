namespace SquashPointAPI.Models;

public class Game
{
    public int Id { get; set; }
    public League League { get; set; }
    public Player? Winner { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public ICollection<PlayerGame> PlayerGames { get; set; }
    public ICollection<Set> Sets { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}