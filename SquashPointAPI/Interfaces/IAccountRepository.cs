using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IAccountRepository
{
    Task<Player> LoginUserAsync(string email);
    Task<bool> FollowPlayerAsync(FollowerFollowee playerFollow);
    Task<bool> UnollowPlayerAsync(Player follower, Player followee);
    Task<bool> SendFriendRequestAsync(PlayerFriend playerFriend);
    Task<bool> AcceptFriendRequestAsync(Player player, Player friend, int status);
    Task<bool> DeleteFriendAsync(Player player, Player friend);
    Task<Player> UpdatePlayerPhoto(string playerId, Image image);
}