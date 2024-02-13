using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.Point;

public class PointDto
{
    public int Id { get; set; }
    public string PointType { get; set; }
    public DateTime CreatedAt { get; set; }
    public PlayerDto Winner { get; set; }
}