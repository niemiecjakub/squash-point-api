using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.Set;

public class SetSummaryDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Winner { get; set; }
    public PlayerSetPointDto Player1 { get; set; }
    public PlayerSetPointDto Player2 { get; set; }
}