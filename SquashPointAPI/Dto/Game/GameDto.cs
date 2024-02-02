using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.Game;

public class GameDto
{
    public int Id { get; set; }
    public string Status { get; set; }
    public PlayerDto? Winner { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
}