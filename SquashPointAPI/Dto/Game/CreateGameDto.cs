namespace SquashPointAPI.Dto.Game;

public class CreateGameDto
{
    public int LeagueId { get; set; }
    public int Player1Id { get; set; }
    public int Player2Id { get; set; }
    public DateTime Date { get; set; }
}