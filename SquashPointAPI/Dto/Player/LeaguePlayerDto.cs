namespace SquashPointAPI.Dto.Player;

public class LeaguePlayerDto : PlayerDto
{
    public int Score { get; set; }
    public int GamesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int GamesLost { get; set; }
}