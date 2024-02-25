using System.ComponentModel.DataAnnotations.Schema;

namespace SquashPointAPI.Models;

[Table(("PlayerGame"))]
public class PlayerGame
{
    public string PlayerId { get; set; }
    public int GameId { get; set; }
    public Player Player { get; set; }
    public Game Game { get; set; }
}