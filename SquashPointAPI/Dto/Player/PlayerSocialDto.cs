using System.Collections;

namespace SquashPointAPI.Dto.Player;

public class PlayerSocialDto
{
    public ICollection<PlayerDto> Followers { get; set; }
    public ICollection<PlayerDto> Following { get; set; }
    public ICollection<PlayerDto> Friends { get; set; }
    public ICollection<PlayerDto> SentFriendRequests { get; set; }
    public ICollection<PlayerDto> ReceivedFriendRequests { get; set; }
}