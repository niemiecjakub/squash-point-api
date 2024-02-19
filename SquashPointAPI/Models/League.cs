namespace SquashPointAPI.Models;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<PlayerLeague> PlayerLeagues { get; set; }
    public ICollection<Game> Games { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}