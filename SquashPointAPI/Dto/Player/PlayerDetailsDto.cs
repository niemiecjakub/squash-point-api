using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.League;

namespace SquashPointAPI.Dto.Player;

public class PlayerDetailsDto : PlayerDto
 {
     public string Sex { get; set; }
     public string Email { get; set; }
     public List<LeagueDto> Leagues { get; set; }
     public List<GameDto> Games { get; set; }
 }