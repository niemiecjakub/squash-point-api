using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPlayerRepository
{
    Task<ICollection<Player>> GetPlayersAsync();
    Task<Player> GetPlayerAsync(string playerId);
    Task<ICollection<Game>> GetAllPlayerGamesAsync(string playerId);
    Task<Player> CreatePlayerAsync(Player player);
    Task<bool> PlayerExistsAsync(string playerId);
    Task<bool> EmailAlreadyTakenAsync(string email);
}