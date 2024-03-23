using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SquashPointAPI.Models;

[Table("FollowerFollowee")]
public class FollowerFollowee
{
    public Player Follower { get; set; }
    public Player Followee { get; set; }

    public string FollowerId { get; set; }
    public string FolloweeId { get; set; }
}