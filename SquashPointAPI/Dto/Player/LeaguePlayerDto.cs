namespace SquashPointAPI.Dto.Player;

public class LeaguePlayerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Sex { get; set; }
    public string Email { get; set; }
    public int Score { get; set; }
    public int GamesPlayed { get; set; }
}