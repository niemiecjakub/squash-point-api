using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.League;

public class LeagueDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PlayerDto> Players { get; set; }
    public List<GameDetailsDto> Games { get; set; }
}