namespace SquashPointAPI.Dto.Set;

public class CreateSetDto
{
    public int Id { get; set; }
    public int? WinnerId { get; set; }
    public int GameId { get; set; }
    public DateTime CreatedAt { get; set; } 
}