using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPlayerRepository
{
    Task<ICollection<Player>> GetPlayersAsync();
    Task<Player> GetPlayerAsync(string playerId);
    Task<ICollection<Game>> GetPlayerGamesAsync(string playerId);
    Task<ICollection<League>> GetPlayerLeagues(string playerId);
    Task<bool> PlayerExistsAsync(string playerId);
}