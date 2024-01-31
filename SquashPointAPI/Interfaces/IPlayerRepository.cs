using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPlayerRepository
{
    Task<ICollection<Player>> GetPlayersAsync();
    Task<ICollection<Player>> GetPlayersAsync(string firstName, string lastName);
    Task<Player> GetPlayerAsync(int playerId);
    Task<Player> CreatePlayerAsync(Player player);
    Task<bool> PlayerExistsAsync(int playerId);
}
