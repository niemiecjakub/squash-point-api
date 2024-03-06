using SquashPointAPI.Dto.Set;

namespace SquashPointAPI.Dto.Game;

public class GameSummaryDto
{
    public int Id { get; set; }
    public string League { get; set; }
    public string? Winner { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<SetSummaryDto>? Sets { get; set; }
}