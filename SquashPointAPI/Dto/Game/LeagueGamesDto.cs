namespace SquashPointAPI.Dto.Game;

public class LeagueGamesDto
{
    public List<GameDto> UpcommingGames { get; set; }
    public List<FinishedGameDto> FinishedGames { get; set; }
    public List<GameDto> LiveGames { get; set; }
}