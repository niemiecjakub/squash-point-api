namespace SquashPointAPI.Dto.Game;

public class PlayerGamesDto
{
    public List<FinishedGameDto> LastGames { get; set; }
    public List<GameDto> NextGames { get; set; }
}