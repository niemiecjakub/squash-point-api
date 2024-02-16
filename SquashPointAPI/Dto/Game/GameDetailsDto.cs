using SquashPointAPI.Dto.League;
using SquashPointAPI.Dto.Player;
using SquashPointAPI.Dto.Set;

namespace SquashPointAPI.Dto.Game;

public class GameDetailsDto
{
    public int Id { get; set; }
    public LeagueDto League { get; set; }
    public string? Status { get; set; }
    public PlayerDto? Winner { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<PlayerDto> Players { get; set; }
    public ICollection<SetDto> Sets { get; set; }
    public int Player1Sets { get; set; }
    public int Player2Sets { get; set; }
}