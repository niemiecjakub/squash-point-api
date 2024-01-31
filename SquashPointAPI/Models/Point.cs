namespace SquashPointAPI.Models;

public class Point
{
    public int Id { get; set; }
    public Player Winner { get; set; }
    public string PointType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}