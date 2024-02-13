namespace SquashPointAPI.Dto.Point;

public class CreatePointDto
{
    public int Id { get; set; }
    public int WinnerId { get; set; }
    public string PointType { get; set; }
    public int SetId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}