namespace SquashPointAPI.Models;

public class Point
{
    public int Id { get; set; }
    public Player Player { get; set; }
    public Set Set { get; set; }
    public int PointScore { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
}