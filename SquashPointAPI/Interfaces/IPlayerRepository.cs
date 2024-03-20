using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPlayerRepository
{
    Task<ICollection<Player>> GetPlayersAsync();
    Task<Player> GetPlayerAsync(string playerId);
    Task<ICollection<Game>> GetPlayerGamesAsync(string playerId);
    Task<ICollection<Game>> GetPlayerFinishedGamesAsync(string playerId);
    Task<ICollection<League>> GetPlayerLeagues(string playerId);
    Task<ICollection<Player>> GetPlayerFriendsAsync(string playerId);
    Task<ICollection<Player>> GetPlayerFollowersAsync(string playerId);
    Task<ICollection<Player>> GetPlayerFolloweesAsync(string playerId);
    Task<bool> FollowPlayerAsync(FollowerFollowee playerFollow);
    Task<bool> UnollowPlayerAsync(Player follower, Player followee);
    Task<Player> LoginUserAsync(string email);

    Task<bool> PlayerExistsAsync(string playerId);
}