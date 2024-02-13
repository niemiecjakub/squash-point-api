using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.League;

namespace SquashPointAPI.Dto.Player;

public class PlayerDetailsDto
 {
     public int Id { get; set; }
     public string FirstName { get; set; }
     public string LastName { get; set; }
     public string FullName { get; set; }
     public string Sex { get; set; }
     public string Email { get; set; }
     public List<LeagueDto> Leagues { get; set; }
     public List<GameDetailsDto> Games { get; set; }
 }