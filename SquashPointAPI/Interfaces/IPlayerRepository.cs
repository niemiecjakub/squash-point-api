using SquashPointAPI.Models;

namespace SquashPointAPI.Interfaces;

public interface IPlayerRepository
{
    ICollection<Player> GetPlayers();
    ICollection<Player> GetPlayers(string firstName, string lastName);
    Player GetPlayer(int playerId);
    bool PlayerExists(int playerId);
    bool CreatePlayer(Player player);
    bool Save();
}