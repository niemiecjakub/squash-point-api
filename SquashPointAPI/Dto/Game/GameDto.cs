using SquashPointAPI.Dto.League;
using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.Game;

public class GameDto
{
    public int Id { get; set; }
    public string? League { get; set; }
    public string? Status { get; set; }
    public string? Winner { get; set; }
    public string Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<PlayerDto> Players { get; set; }
}