namespace SquashPointAPI.Dto.Game;

public class FinishedGameDto
{
    public int Id { get; set; }
    public string League { get; set; }
    public string Winner { get; set; }
    public string Status { get; set; }
    public ICollection<PlayerGameSummary> Players { get; set; }
}

public class PlayerGameSummary
{
    public string FullName { get; set; }
    public int Sets { get; set; }
    public byte[]? Photo { get; set; }
}