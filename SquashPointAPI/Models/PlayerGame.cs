namespace SquashPointAPI.Models;

public class PlayerGame
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int GameId { get; set; }
    public Player Player { get; set; }
    public Game Game { get; set; }
}