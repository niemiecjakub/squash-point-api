namespace SquashPointAPI.Dto.League;

public class UpdateLeagueDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int MaxPlayers { get; set; }
    public bool Public { get; set; }
}