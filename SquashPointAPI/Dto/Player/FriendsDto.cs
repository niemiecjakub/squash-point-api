namespace SquashPointAPI.Dto.Player;

public class FriendsDto
{
    public ICollection<PlayerDto> Friends { get; set; }
    public ICollection<PlayerDto> Requests { get; set; }
}