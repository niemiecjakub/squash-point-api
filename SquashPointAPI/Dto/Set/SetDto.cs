using SquashPointAPI.Dto.Player;
using SquashPointAPI.Dto.Point;

namespace SquashPointAPI.Dto.Set;

public class SetDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Winner { get; set; }
    public ICollection<PointDto> Points { get; set; } = new List<PointDto>();
}