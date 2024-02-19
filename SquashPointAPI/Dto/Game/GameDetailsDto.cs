using SquashPointAPI.Dto.Set;

namespace SquashPointAPI.Dto.Game;

public class GameDetailsDto : GameDto
{
    public ICollection<SetDto> Sets { get; set; }
    public int Player1Sets { get; set; }
    public int Player2Sets { get; set; }
}