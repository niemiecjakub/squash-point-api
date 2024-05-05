namespace SquashPointAPI.Dto.League;

public class LeagueDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Owner { get; set; }
    public int MaxPlayers { get; set; }
    public int PlayerCount { get; set; }
    public bool Public { get; set; }
    public string? Description { get; set; }
}