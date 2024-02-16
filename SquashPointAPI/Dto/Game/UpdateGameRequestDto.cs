namespace SquashPointAPI.Dto.Game;

public class UpdateGameRequestDto
{
    public string? Status { get; set; } = null;
    public int? WinnerId { get; set; } = null;
}