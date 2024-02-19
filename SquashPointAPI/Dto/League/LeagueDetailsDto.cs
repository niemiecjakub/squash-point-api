using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.League;

public class LeagueDetailsDto : LeagueDto
{
    public List<LeaguePlayerDto> Players { get; set; }
    public List<GameDto> Games { get; set; }
}