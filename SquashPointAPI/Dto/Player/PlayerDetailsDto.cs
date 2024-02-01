using SquashPointAPI.Dto.League;

namespace SquashPointAPI.Dto.Player;

public class PlayerDetailsDto
 {
     public int Id { get; set; }
     public string FirstName { get; set; }
     public string LastName { get; set; }
     public string Sex { get; set; }
     public List<LeagueDto> Leagues { get; set; }
 }