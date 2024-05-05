using SquashPointAPI.Dto.Game;
using SquashPointAPI.Dto.League;

namespace SquashPointAPI.Dto.Player;

public class PlayerDetailsDto : PlayerDto
{
    public string Sex { get; set; }
    public string Email { get; set; }
    public int Followers { get; set; }
    public int Following { get; set; }
    public int Friends { get; set; }
    public byte[]? Photo { get; set; }
}