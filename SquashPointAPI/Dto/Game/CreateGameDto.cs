using System.ComponentModel.DataAnnotations;

namespace SquashPointAPI.Dto.Game;

public class CreateGameDto
{
    [Required]
    public int LeagueId { get; set; }
    [Required]
    public string opponentId { get; set; }
    [Required]
    public DateTime Date { get; set; }
}