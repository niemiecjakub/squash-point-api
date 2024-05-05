using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.League;

public class LeagueDetailsDto : LeagueDto
{
    public byte[]? Photo { get; set; }
}