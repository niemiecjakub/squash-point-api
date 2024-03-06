using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.Set;

public class SetSummaryDto
{
    public DateTime CreatedAt { get; set; }
    public PlayerDto? Winner { get; set; }
    public int Player1Points { get; set; }
    public int Player2Points { get; set; }
}