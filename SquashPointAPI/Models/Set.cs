namespace SquashPointAPI.Models;

public class Set
{
    public int Id { get; set; }
    public Player? Winner { get; set; }
    public Game Game { get; set; }
    public ICollection<Point> Points { get; set; } = new List<Point>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}