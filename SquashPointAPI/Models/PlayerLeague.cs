namespace SquashPointAPI.Models;

public class PlayerLeague
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int LeagueId { get; set; }
    public Player Player { get; set; }
    public League League { get; set; }
}