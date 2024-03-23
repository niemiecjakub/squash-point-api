namespace SquashPointAPI.Dto.League;

public class CreateLeagueDto
{
    public string Name { get; set; }
    public int MaxPlayers { get; set; }
    public bool Public { get; set; }
    public string Description { get; set; }
}