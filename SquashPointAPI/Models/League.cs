namespace SquashPointAPI.Models;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Player Owner { get; set; }
    public string Description { get; set; }
    public int MaxPlayers { get; set; }
    public bool Public { get; set; }
    public Image? Photo { get; set; }
    public int? PhotoId { get; set; }
    public ICollection<PlayerLeague> PlayerLeagues { get; set; }
    public ICollection<Game> Games { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}