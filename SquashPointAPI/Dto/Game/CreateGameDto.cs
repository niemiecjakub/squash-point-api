namespace SquashPointAPI.Dto.Game;

public class CreateGameDto
{
    public int LeagueId { get; set; }
    public string Player1Id { get; set; }
    public string Player2Id { get; set; }
    public DateTime Date { get; set; }
}