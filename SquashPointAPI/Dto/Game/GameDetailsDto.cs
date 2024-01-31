using SquashPointAPI.Dto.Player;

namespace SquashPointAPI.Dto.Game;

public class GameDetailsDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<PlayerDto> players { get; set; }
}