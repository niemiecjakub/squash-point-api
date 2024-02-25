using Microsoft.AspNetCore.Identity;

namespace SquashPointAPI.Models;

public class Player : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Sex { get; set; }
    public ICollection<PlayerLeague> PlayerLeagues { get; set; } = new List<PlayerLeague>();
    public ICollection<PlayerGame> PlayerGames { get; set; } = new List<PlayerGame>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}