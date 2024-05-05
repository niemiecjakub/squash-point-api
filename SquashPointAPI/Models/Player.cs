using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SquashPointAPI.Models;

public class Player : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Sex { get; set; }
    public Image? Photo { get; set; }
    public int? PhotoId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<PlayerLeague> PlayerLeagues { get; set; } = new List<PlayerLeague>();
    public ICollection<PlayerGame> PlayerGames { get; set; } = new List<PlayerGame>();
    public virtual ICollection<PlayerFriend> Friends { get; set; } = new List<PlayerFriend>();
    public virtual ICollection<FollowerFollowee> Following { get; set; } = new List<FollowerFollowee>();
    
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}