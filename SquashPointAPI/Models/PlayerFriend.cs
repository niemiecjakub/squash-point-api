using System.ComponentModel.DataAnnotations.Schema;

namespace SquashPointAPI.Models;

[Table(("PlayerFriend"))]
public class PlayerFriend
{
    public Player Player { get; set; }
    public Player Friend { get; set; }

    public string PlayerId { get; set; }
    public string FriendId { get; set; }

    public int Status { get; set; }
}