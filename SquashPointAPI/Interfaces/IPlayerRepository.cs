using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPlayerRepository
{
    Task<ICollection<Player>> GetPlayersAsync();
    Task<Player> GetPlayerAsync(int playerId);
    Task<ICollection<Game>> GetAllPlayerGamesAsync(int playerId);
    Task<Player> CreatePlayerAsync(Player player);
    Task<bool> PlayerExistsAsync(int playerId);
    Task<bool> EmailAlreadyTakenAsync(string email);
}