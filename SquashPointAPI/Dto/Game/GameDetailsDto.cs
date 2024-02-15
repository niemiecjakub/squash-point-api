using SquashPointAPI.Dto.Player;
using SquashPointAPI.Dto.Point;
using SquashPointAPI.Dto.Set;

namespace SquashPointAPI.Dto.Game;

public class GameDetailsDto
{
    public int Id { get; set; }
    public int LeagueId { get; set; }
    public string Status { get; set; }
    public PlayerDto? Winner { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<PlayerDto> Players { get; set; }

    public ICollection<SetDto> Sets { get; set; }
}