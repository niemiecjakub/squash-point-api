using System.ComponentModel.DataAnnotations.Schema;

namespace SquashPointAPI.Models;

[Table(("PlayerLeague"))]
public class PlayerLeague
{
    public string PlayerId { get; set; }
    public int LeagueId { get; set; }
    public Player Player { get; set; }
    public League League { get; set; }
    public int Score { get; set; }
}